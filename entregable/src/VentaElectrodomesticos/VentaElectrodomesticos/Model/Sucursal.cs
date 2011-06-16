using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    class Sucursal
    {
        public byte provincia { get; set; }
        public byte tipo { get; set; }
        public String direccion { get; set; }
        public String telefono { get; set; }

        public Sucursal() { }
        public Sucursal(byte provincia, byte tipo, String direccion, String telefono)
        {
            this.provincia = provincia;
            this.tipo = tipo;
            this.direccion = direccion;
            this.telefono = telefono;
        }
    }
}
