using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    class Provincias : DAO
    {
        public static Provincias instance;
        public static Provincias getInstance() {
            if (instance == null)
                {
                    instance = new Provincias();
                }
                return instance;
        }
        private Provincias() { this.initialize(); }
        private Dictionary<byte, Provincia> provincias = new Dictionary<byte, Provincia>();
        protected override String listQuery()
        {
            return "SELECT Codigo, Nombre FROM " + ClaseSQL.tableName("Provincias");
        }

        protected override void parseResult(SqlDataReader reader)
        {
            byte codigo = reader.GetByte(0);
            String nombre = reader.GetString(1);
            Provincia provincia = new Provincia(codigo, nombre);
            provincias.Add(codigo, provincia);
        }

        public List<Provincia> list()
        {
            return provincias.Values.ToList();
        }

        public Provincia provincia(byte codigo)
        {
            return provincias[codigo];
        }
    }
}
