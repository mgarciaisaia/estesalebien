using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Empleado
    {
        public decimal dni;
        public String nombre;
        public String apellido;
        public String mail;
        public String telefono;
        public String direccion;
        public byte provincia;
        public byte tipo;
        public byte sucursal;
        public Boolean habilitado;

        public Empleado() { }

        public Empleado(Object[] arrayFields)
        {
            this.dni = (decimal) arrayFields[0];
            this.nombre = (String) arrayFields[1];
            this.apellido = (String)arrayFields[2];
            this.mail = (String)arrayFields[3];
            this.telefono = (String)arrayFields[4];
            this.direccion = (String)arrayFields[5];
            this.provincia = (byte)arrayFields[6];
            this.tipo = (byte)arrayFields[7];
            this.sucursal = (byte)arrayFields[8];
            this.habilitado = (byte)arrayFields[9] != 0;
        }
    }
}
