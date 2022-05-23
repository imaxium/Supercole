using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supercole.Clases
{
    class Clase
    {
        private string id_clase;
        private string clase_nombre;

        public Clase(string id_clase, string clase_nombre)
        {
            this.id_clase = id_clase;
            this.clase_nombre = clase_nombre;
        }

        public string Id_clase { get => id_clase; set => id_clase = value; }
        public string Clase_nombre { get => clase_nombre; set => clase_nombre = value; }
    }
}
