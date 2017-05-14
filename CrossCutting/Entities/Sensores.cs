using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Sensores
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        //public string NombreTipo { get; set; }
        public string Descripcion { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
    }
}
