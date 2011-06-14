using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.AbmCliente
{
    public class Cliente
    {
        private string dni;
        public string DNI
        {
            get { return dni; }
            set { dni = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        private string tefono;
        public string Telefono
        {
            get { return tefono; }
            set { tefono = value; }
        }

        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private string provincia;
        public string Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        private bool habilitado;
        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }

    }
}
