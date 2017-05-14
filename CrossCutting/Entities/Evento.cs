using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Nombre { get; set; }
        public string ChannelName { get; set; }
        public string ValorLimite { get; set; }
        public string Operador { get; set; }
        public string TipoDato { get; set; }
    }
}
