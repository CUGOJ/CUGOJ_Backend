using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.VersionContainer
{
    /// <summary>
    /// 分发中心真正的数据存储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVersionItemStorage<T> where T : IVersionItem
    {
        /// <summary>
        /// 获取所有数据，用于冷启动同步
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllVersionItems();
        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task PushVersionItems(IEnumerable<T> items);
        /// <summary>
        /// 存储模块初始化
        /// </summary>
        /// <returns></returns>
        public Task Init();
    }
}
