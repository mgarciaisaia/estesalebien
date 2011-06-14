using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VentaElectrodomesticos.Model
{
    public class Empleado
    {
        public Empleado(Object algo)
        {
            String bleh = algo.ToString();
            bleh.Replace("", "asfda");
        }
    }
}
