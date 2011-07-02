using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Rol
    {
        public int codigo { get; set; }
        public String nombre { get; set; }
        public Boolean habilitado {get; set; }
		
        public Rol() {
            this.codigo = 0;
            this.nombre = "";
            this.habilitado = false;
        }

        public Rol(int codigo, String nombre, Boolean habilitado)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.habilitado = habilitado;
        }

        public Rol(Object[] arrayFields)
        {
            this.codigo = (int)arrayFields[0];
            this.nombre = (String)arrayFields[1];
            this.habilitado = (byte)arrayFields[4] != 0;
        }
    }
}
