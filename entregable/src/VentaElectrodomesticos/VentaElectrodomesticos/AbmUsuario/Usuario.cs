using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.AbmUsuario
{
    public class Usuario
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string empleado;
        public string Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }

        private bool habilitado;
        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }

        private string intentos;
        public string Intentos
        {
            get { return intentos; }
            set { intentos = value; }
        }

        
    }
}
