using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Rol
    {
        public int codigo;
        public String nombre;
        public Boolean habilitado;
		
        public Rol() { }

        public Rol(Object[] arrayFields)
        {
            this.codigo = (int)arrayFields[0];
            this.nombre = (String)arrayFields[1];
            this.habilitado = (byte)arrayFields[4] != 0;
        }
    }
}
