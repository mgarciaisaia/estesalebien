using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.DAO
{
    class Marca
    {
        public int codigo { get; set; }
        public String nombre { get; set; }
        public Marca() { }
        public Marca(int codigo, String nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }
    }
}
