using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Producto
    {
        public int codigo;
        public String nombre;
        public String descripcion;
        public int categoria;
		public float precio;
		public Boolean habilitado;
		public int marca;
        
        public Producto() { }

        public Producto(Object[] arrayFields)
        {
            this.codigo = (int)arrayFields[0];
            this.nombre = (String)arrayFields[1];
            this.descripcion = (String)arrayFields[2];
            this.categoria = (int)arrayFields[3];
            this.precio = (float)arrayFields[4];
			this.habilitado = (byte)arrayFields[5] != 0;
            this.marca = (int)arrayFields[6];
        }
    }
}
