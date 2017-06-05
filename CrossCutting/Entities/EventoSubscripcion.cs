using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class EventoSubscripcion
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int UsuarioId { get; set; }
        public int Radio { get; set; }
        public float CentroLatitud { get; set; }
        public float CentroLongitud { get; set; }
    }
}
