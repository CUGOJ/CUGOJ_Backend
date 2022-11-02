using CUGOJ.Backend.Share;
using CUGOJ.Backend.Share.Infra;
using CUGOJ.Backend.Tools.Log;
using CUGOJ.Backend.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Base.Infra.ConfigProvider
{
    // Singleton Service
    public class ConfigProvider : IConfigProvider
    {
        private readonly Logger _logger;
        private readonly IHttpProvider _httpProvider;
        public ConfigProvider(Logger logger, IHttpProvider httpProvider)
        {
            _logger = logger;
            _httpProvider = httpProvider;
            timer = new Timer(UpdateConfig, null, 0, 10000);
        }
        
        private readonly Timer timer;
        private Dictionary<string, string> store = new();

        private long version = 0;
        public long Version { get => version; }

        private bool Equals(Dictionary<string,string>?d1,Dictionary<string,string>?d2)
        {
            if (d1 == null || d2 == null) return false;
            if (d1.Keys.Count != d2.Keys.Count) return false;
            foreach(var item in d1)
            {
                if (!d2.ContainsKey(item.Key))
                    return false;
                if (!item.Value.Equals(d2[item.Key]))
                    return false;
            }
            return true;
        }

        private async void UpdateConfig(object ? e)
        {
            var getConfigResult = await _httpProvider.Post<Dictionary<string, string>>(Config.GetApiAddress(ApiList.GetConfig), Config.Env);
            if (getConfigResult.IsSuccess)
            {
                if (getConfigResult.Data==null)
                {
                    _logger.Error($"获取到的配置信息为空,code = {getConfigResult.Code},msg = {getConfigResult.Message}");
                    return;
                }
                if (!Equals(store,getConfigResult.Data))
                {
                    version = (long)(DateTime.Now - DateTime.UnixEpoch).TotalMilliseconds;
#if DEBUG
                    var changeRecord = "";
                    HashSet<string> keySet = new();
                    foreach (var k in store.Keys)
                        keySet.Add(k);
                    foreach (var k in getConfigResult.Data.Keys)
                        keySet.Add(k);
                    foreach(var key in keySet)
                    {
                        if (store.ContainsKey(key))
                        {
                            if(!getConfigResult.Data.ContainsKey(key))
                            {
                                changeRecord += $"{key} : {store[key]} -> null\n";
                            }
                            else
                            {
                                changeRecord += $"{key} : {store[key]} -> {getConfigResult.Data[key]}\n";
                            }
                        }
                        else
                        {
                            changeRecord += $"{key} : null -> {getConfigResult.Data[key]}\n";
                        }
                    }
                    _logger.Info($"配置更新,store, changeRecord = {changeRecord}");
#endif
                    store = getConfigResult.Data;
                }
            }
            else
            {
                _logger.Error($"未能成功获取配置信息,code = {getConfigResult.Code},msg = {getConfigResult.Message}");
            }
        }
        public async Task<object?> Get(string key,Type type)
        {
            string? value;
            if (!store.TryGetValue(key, out value) || value==null)
                return default;
            return await Task.Run(() =>
            {
                try
                {
                    return Convert.ChangeType(value, type);
                }
                catch (Exception)
                {
                    _logger.Error($"配置读取出错,key={key},value={value},期望的类型为:{type.Name}");
                    return null;
                }
            });
        }

        public Task<bool> IsSetEnable()
        {
            return Task.FromResult(false);
        }

        public async Task<bool> Set<T>(string key) where T : IConvertible
        {
            if (!await IsSetEnable())
                return false;
            return false;
        }
    }
}
