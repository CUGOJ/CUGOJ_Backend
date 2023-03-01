using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    public interface IAuthorize
    {
        public long Id { get; set; }
        public long ToLong();
        public void Init(long _authorize);
    }
}
