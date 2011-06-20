using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.MetodosSQL;
using VentaElectrodomesticos.Model;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace VentaElectrodomesticos.DAO
{
    class Marcas : DAOCache
    {
        private static Marcas instance;
        public static Marcas getInstance()
        {
            if (instance == null)
            {
                instance = new Marcas();
            }
            return instance;
        }

        private Marcas() {
            this.initialize();
        }

        private Dictionary<int, Marca> marcas = new Dictionary<int, Marca>();

        public Marca marca(int codigo)
        {
            return marcas[codigo];
        }

        public List<Marca> lista()
        {
            return marcas.Values.ToList();
        }

        protected override String listQuery()
        {
            return "SELECT Codigo, Nombre FROM " + ClaseSQL.tableName("Marcas");
        }

        protected override void parseResult(SqlDataReader reader)
        {
            int codigo = reader.GetInt32(0);
            String nombre = reader.GetString(1);
            Marca marca = new Marca(codigo, nombre);
            marcas.Add(codigo, marca);
        }
    }
}