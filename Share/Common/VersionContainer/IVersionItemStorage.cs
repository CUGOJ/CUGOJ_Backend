using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.VersionContainer
{
    public interface IVersionItemStorage<T> where T: IVersionItem
    {
        public ReaderWriterLockSlim Lock { get; }
        public Task<IEnumerable<T>> GetAllVersionItems();
        public Task PushVersionItems(IEnumerable<T> items);
        public Task Init();
    }
}
