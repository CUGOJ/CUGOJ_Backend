using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.VersionContainer
{
    public interface IVersionItemManager<T> where T:IVersionItem
    {
        public Task<IEnumerable<T>> GetAllVersionItems();
        public Task PushVersionItems(IEnumerable<T> items);
        public Task<UpdateVersionItemResponse<T>> UpdateVersionItem(long version, long managerVersion);
    }

    [GenerateSerializer]
    public class UpdateVersionItemResponse<T> where T : IVersionItem
    {
        public enum UpdateVersionItemResponseTypeEnum
        {
            Increment,
            Rebase,
            Error,
        }

        [Id(0)]
        public UpdateVersionItemResponseTypeEnum ResponseType { get; set; }
        [Id(1)]
        public IEnumerable<T> VersionItemInfo { get; set; } = null!;
        [Id(2)]
        public long Version { get; set; }
        [Id(3)]
        public long ManagerVerson { get; set; }
    }
}
