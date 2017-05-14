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
        public int ZonaId { get; set; }
        public int EventoId { get; set; }
        public int UsuarioId { get; set; }
    }
}
