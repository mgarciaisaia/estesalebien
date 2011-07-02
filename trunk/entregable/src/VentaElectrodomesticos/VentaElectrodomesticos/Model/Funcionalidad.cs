using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.DAO
{
    class Funcionalidad
    {
        public byte codigo { get; set; }
        public String descripcion {get;set;}
        public Funcionalidad() { }
        public Funcionalidad(byte codigo, String descripcion)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
        }
    }
}
