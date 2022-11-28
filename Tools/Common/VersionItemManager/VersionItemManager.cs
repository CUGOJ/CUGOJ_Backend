using CUGOJ.Backend.Share.Common.VersionContainer;
using CUGOJ.Backend.Tools.Log;
using Orleans.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Tools.Common.VersionContainer
{
    public class VersionItemManager<T> : IVersionItemManager<T> where T : IVersionItem
    {
        readonly Logger? _logger;
        readonly IVersionItemStorage<T> _storage;
        private long _managerVersion = CommonTools.GetUTCUnixMilli();
        readonly LinkedList<T> versionItems = new();
        readonly Dictionary<long, LinkedListNode<T>> versionItemsIndex = new();
        public VersionItemManager(IVersionItemStorage<T> storage,Logger? logger)
        {
            _storage = storage;
            _logger = logger;
        }
        public Task<IEnumerable<T>> GetAllVersionItems()
        {
            if(_storage.Lock.TryEnterReadLock(Config.VersionItemMangerReaderLockTimeout))
            {
                try
                {
                    return _storage.GetAllVersionItems();
                }
                finally
                {
                    _storage.Lock.ExitReadLock();
                }
            }
            else
            {
                throw new Exception("服务繁忙,请稍后再试");
            }
        }

        public async Task PushVersionItems(IEnumerable<T> items)
        {
            if (_storage.Lock.TryEnterWriteLock(Config.VersionItemManagerWriterLockTimeout))
            {
                try
                {
                    var version = CommonTools.GetUTCUnixMilli();
                    foreach(var item in items)
                    {
                        item.Version = version;
                        versionItems.AddLast(item);
                    }
                    await _storage.PushVersionItems(items);
                    if (versionItems.Last != null)
                        versionItemsIndex[version] = versionItems.Last;
                    LimitVersionItems();
                }
                catch(Exception e)
                {
                    _logger?.Error($"PushVersionItemsError:{e}");
                    versionItems.Clear();
                    versionItemsIndex.Clear();
                    _managerVersion = CommonTools.GetUTCUnixMilli();
                    throw;
                }
                finally
                {
                    _storage.Lock.ExitWriteLock();
                }
            }
            else
            {
                throw new Exception("服务繁忙,请稍后再试");
            }
        }

        private async Task<UpdateVersionItemResponse<T>>GetRebaseResp()
        {
            UpdateVersionItemResponse<T> resp = new()
            {
                ManagerVerson = _managerVersion,
            };
            resp.ResponseType = UpdateVersionItemResponse<T>.UpdateVersionItemResponseTypeEnum.Rebase;
            resp.VersionItemInfo = await _storage.GetAllVersionItems();
            foreach (var item in resp.VersionItemInfo)
            {
                resp.Version = Math.Max(resp.Version, item.Version);
            }
            resp.ResponseType = UpdateVersionItemResponse<T>.UpdateVersionItemResponseTypeEnum.Rebase;
            return resp;
        }

        private async Task<UpdateVersionItemResponse<T>>GetIncrementResp(long version)
        {
            if (!versionItemsIndex.TryGetValue(version, out LinkedListNode<T>? item))
            {
                _logger?.Warn($"索引丢失,verson={version},T={typeof(T)}");
                item = null;
                var node = versionItems.First;
                while (node != null)
                {
                    if (node.Value.Version > version)
                    {
                        item = node;
                        break;
                    }
                    node = node.Next;
                }
            }
            if (item == null) 
            {
                _logger?.Error($"索引丢失,version = {version},T={typeof(T).Name}");
                return await GetRebaseResp();
            }
            else if (item.Value.Version!=version || (item.Next!=null && item.Next.Value.Version <= version))
            {
                _logger?.Error($"索引数据有误,T={typeof(T).Name},索引={CommonTools.ToJsonString(item)}");
                return await GetRebaseResp();
            }
            var respItems = new List<T>();
            UpdateVersionItemResponse<T> resp = new()
            {
                ManagerVerson = _managerVersion,
                ResponseType = UpdateVersionItemResponse<T>.UpdateVersionItemResponseTypeEnum.Increment,
            };
            item = item.Next;
            while(item!=null)
            {
                respItems.Add(item.Value);
                resp.Version = Math.Max(resp.Version, item.Value.Version);
            }
            resp.VersionItemInfo = respItems;
            return resp;
        }
        public async Task<UpdateVersionItemResponse<T>> UpdateVersionItem(long version, long managerVersion)
        {
            if (_storage.Lock.TryEnterReadLock(Config.VersionItemMangerReaderLockTimeout))
            {
                try
                {
                    if (managerVersion != _managerVersion )
                    {
                        return await GetRebaseResp();
                    }
                    else if (versionItems.First!=null && versionItems.First.Value.Version>version)
                    {
                        return await GetRebaseResp();
                    }
                    else
                    {
                        return await GetIncrementResp(version);
                    }
                }
                catch(Exception e)
                {
                    _logger?.Error($"PushVersionItemsError:{e}");
                    throw;
                }
                finally
                {
                    _storage.Lock.ExitReadLock();
                }
            }
            else
            {
                throw new Exception("服务繁忙,请稍后再试");
            }
        }

        private void PopVerisonItem()
        {
            var value = versionItems.First;
            if (value == null)
                return;
            if (value.Next == null || value.Next.Value.Version != value.Value.Version)
            {
                versionItemsIndex.Remove(value.Value.Version);
            }
            versionItems.RemoveFirst();
        }
        private void LimitVersionItems()
        {
            var versionItemsLimit = Config.VersionItemSnapshotLimit;
            if (versionItemsLimit > 0)
            {
                while (versionItems.Count > versionItemsLimit)
                    PopVerisonItem();
            }
        }
    }
}
