using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    class TipoEmpleado
    {
        public byte codigo { get; set; }
        public String descripcion { get; set; }
        public TipoEmpleado(byte codigo, String descripcion)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
        }
    }
}
