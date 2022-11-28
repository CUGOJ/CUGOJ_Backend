using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Backend.Share.Common.Authorize
{
    public interface IAuthorize
    {
        public long ToLong();
        public void Init(long _authorize);
    }
}
