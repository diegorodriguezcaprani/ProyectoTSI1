using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class ResponseFront
    {
        public Boolean status { get; set; }
        public string errmsg { get; set; }
        public string valor { get; set; }
    }
}
