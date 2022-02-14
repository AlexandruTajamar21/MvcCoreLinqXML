using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Models
{
    public class Joyeria
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string CIF { get; set; }
        public string Direccion { get; set; }
        public object Add { get; internal set; }
    }
}
