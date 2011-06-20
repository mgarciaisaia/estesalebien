using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;
using System.Data;

namespace VentaElectrodomesticos.DAO
{
    class TiposEmpleado : DAOCache
    {
        public static TiposEmpleado instance;
        public static TiposEmpleado getInstance() {
            if (instance == null)
                {
                    instance = new TiposEmpleado();
                }
                return instance;
        }
        private TiposEmpleado() { this.initialize(); }
        private Dictionary<int, TipoEmpleado> tipos = new Dictionary<int, TipoEmpleado>();
        protected override String listQuery()
        {
            return "SELECT Codigo, Descripcion FROM " + ClaseSQL.tableName("TiposEmpleado");
        }

        protected override void parseResult(SqlDataReader reader)
        {
            byte codigo = reader.GetByte(0);
            String descripcion = reader.GetString(1);
            TipoEmpleado tipoEmpleado = new TipoEmpleado(codigo, descripcion);
            tipos.Add(codigo, tipoEmpleado);
        }

        
        public TipoEmpleado tipoEmpleado(int codigo)
        {
            return tipos[codigo];
        }

        public TipoEmpleado tipoEmpleado(String descripcion)
        {
            foreach (TipoEmpleado tipo in tipos.Values)
            {
                if (tipo.descripcion.Equals(descripcion))
                {
                    return tipo;
                }
            }
            throw new Exception("No existe tipo de empleado con descripcion " + descripcion);
        }

        public List<TipoEmpleado> tiposEmpleado()
        {
            return tipos.Values.ToList();
        }
    }
}
