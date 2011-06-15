using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Usuario
    {
        public int codigo;
        public String nombre;
        public String password;
        public decimal empleado;
		public Boolean habilitado;
		public byte intentos;
        
        public Usuario() { }

        public Usuario(Object[] arrayFields)
        {
            this.codigo = (int)arrayFields[0];
            this.nombre = (String)arrayFields[1];
            this.password = (String)arrayFields[2];
            this.empleado = (decimal)arrayFields[3];
            this.habilitado = (byte)arrayFields[4] != 0;
            this.intentos = (byte)arrayFields[5];
        }
    }
}
