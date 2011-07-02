using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    class Funcionalidades : DAOCache
    {
        public static Funcionalidades instance;
        public static Funcionalidades getInstance() {
            if (instance == null)
                {
                    instance = new Funcionalidades();
                }
                return instance;
        }
        private Funcionalidades() { this.initialize(); }
        private Dictionary<byte, Funcionalidad> funcionalidades = new Dictionary<byte, Funcionalidad>();
        protected override String listQuery()
        {
            return "SELECT Codigo, Descripcion FROM " + ClaseSQL.tableName("Funcionalidades");
        }

        protected override void parseResult(SqlDataReader reader)
        {
            byte codigo = reader.GetByte(0);
            String nombre = reader.GetString(1);
            Funcionalidad provincia = new Funcionalidad(codigo, nombre);
            funcionalidades.Add(codigo, provincia);
        }

        public List<Funcionalidad> list()
        {
            return funcionalidades.Values.ToList();
        }

        public Funcionalidad funcionalidad(byte codigo)
        {
            return funcionalidades[codigo];
        }

        public Funcionalidad funcionalidad(String descripcion)
        {
            foreach (Funcionalidad funcionalidad in funcionalidades.Values)
            {
                if (funcionalidad.descripcion.Equals(descripcion))
                {
                    return funcionalidad;
                }
            }
            throw new Exception("No existe funcionalidad con descripcion " + descripcion);
        }
    }
}
