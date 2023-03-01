using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.VersionContainer
{
    /// <summary>
    /// 增量分发模块
    /// </summary>
    /// <typeparam name="T">增量分发模块要维护的信息，需实现IVersionItem接口</typeparam>
    public interface IVersionItemManager<T> where T : IVersionItem
    {
        /// <summary>
        /// 获取全量信息
        /// </summary>
        /// <returns>全量信息</returns>
        public Task<IEnumerable<T>> GetAllVersionItems();
        /// <summary>
        /// 加入新的信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task PushVersionItems(IEnumerable<T> items);
        /// <summary>
        /// 从分发中心同步，根据参数version和managerVersion会导致分发中心返回不同类型的数据，可以为增量更新，如果version过于老旧将会返回全量数据
        /// </summary>
        /// <param name="version">调用方存储的最新数据版本</param>
        /// <param name="managerVersion">调用方存储的分发中心版本，用于分发中心重启后冷启动的识别</param>
        /// <returns>用于同步的数据</returns>
        public Task<UpdateVersionItemResponse<T>> SynchVersionItem(long version, long managerVersion);
    }

    /// <summary>
    /// 用于从分发中心同步数据的结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GenerateSerializer]
    public class UpdateVersionItemResponse<T> where T : IVersionItem
    {
        public enum UpdateVersionItemResponseTypeEnum
        {
            Increment,// 增量更新
            Rebase, //全量加载
            Error, //出现错误
        }
        /// <summary>
        /// 响应数据类型
        /// </summary>
        [Id(0)]
        public UpdateVersionItemResponseTypeEnum ResponseType { get; set; }
        /// <summary>
        /// 响应的信息主体
        /// </summary>
        [Id(1)]
        public IEnumerable<T> VersionItemInfo { get; set; } = null!;
        /// <summary>
        /// 本次加载到的数据的版本
        /// </summary>
        [Id(2)]
        public long Version { get; set; }
        /// <summary>
        /// 本次作出响应的分发中心的版本
        /// </summary>
        [Id(3)]
        public long ManagerVerson { get; set; }
    }
}
