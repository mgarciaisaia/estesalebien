using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Cliente
    {
        public decimal dni;
        public String nombre;
        public String apellido;
        public String mail;
        public String telefono;
        public String direccion;
        public byte provincia;
        public Boolean habilitado;

        public Cliente() { }

        public Cliente(Object[] arrayFields)
        {
            this.dni = (decimal)arrayFields[0];
            this.nombre = (String)arrayFields[1];
            this.apellido = (String)arrayFields[2];
            this.mail = (String)arrayFields[3];
            this.telefono = (String)arrayFields[4];
            this.direccion = (String)arrayFields[5];
            this.provincia = (byte)arrayFields[6];
            this.habilitado = (byte)arrayFields[7] != 0;
        }
    }
}