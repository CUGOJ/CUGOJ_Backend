using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.Common.User
{
    public partial class UserGrainBase
    {
        public class UserExtra
        {
            public string? Token { get; set; }
        }

    }
}
