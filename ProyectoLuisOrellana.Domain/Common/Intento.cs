using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Domain.Common
{
    public class Intento
    {
        public string Pais { get; set; }
        public string Nombre{ get; set; }

        public string TipoIntento { get; set; }

        public int Peso { get; set; }

        public int? NumeroIntento { get; set; }
    }
}
