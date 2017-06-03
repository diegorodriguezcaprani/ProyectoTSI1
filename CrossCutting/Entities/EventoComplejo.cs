using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class EventoComplejo
    {
        public string Nombre { get; set; }
        public List<Evento> Hijos { get; set; }
        public string Operador { get; set; }
    }
}
