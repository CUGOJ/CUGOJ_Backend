using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.DAO
{
    public interface ITxHandler
    {
        public Task Handle(ICUGOJContext context);
    }

}
