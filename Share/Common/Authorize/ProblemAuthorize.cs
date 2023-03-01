using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Share.Common.Authorize
{
    [GenerateSerializer]
    public class ProblemAuthorize : AuthorizeBase
    { 
        public bool Listable
        {
            get => (auth & 0b01) == 0;
            set
            {
                if (value) auth |= 0b01;
                else auth &= ~0b01;
            }
        }
        public bool Visitable
        {
            get => (auth & 0b10) == 0;
            set
            {
                if (value) auth |= 0b10;
                else auth &= ~0b10;
            }
        }
        public bool Editable
        {
            get => (auth & 0b100) == 0;
            set
            {
                if (value) auth |= 0b100;
                else auth &= ~0b100;
            }
        }

        public static ProblemAuthorize Default => new()
        {

        };
    }
}
