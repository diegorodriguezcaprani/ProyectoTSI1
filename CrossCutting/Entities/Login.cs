using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public string TokenId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
