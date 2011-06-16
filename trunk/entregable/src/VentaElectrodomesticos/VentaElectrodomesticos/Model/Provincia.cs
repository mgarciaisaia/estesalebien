using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.DAO
{
    class Provincia
    {
        public byte codigo { get; set; }
        public String nombre {get;set;}
        public Provincia() { }
        public Provincia(byte codigo, String nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }
    }
}
