using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    class Categoria
    {
        public int codigo { get; set; }
        public String nombre { get; set; }
        public int padre { get; set; }
        public Categoria(int codigo, String nombre, int padre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.padre = padre;
        }
    }
}
