using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class TipoSensor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Frecuencia { get; set; }
        public string TipoDato { get; set; }
    }
}
