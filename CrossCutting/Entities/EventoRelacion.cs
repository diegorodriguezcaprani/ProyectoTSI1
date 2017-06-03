using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class EventoRelacion
    {
        public int EvPadreId { get; set; }
        public int EvHijoId { get; set; }
        public bool Activado { get; set; }
        public string Operador { get; set; }
    }
}
