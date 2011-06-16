using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    class Sucursales : DAO
    {
        public static Sucursales instance;
        public static Sucursales getInstance() {
            if (instance == null)
                {
                    instance = new Sucursales();
                }
                return instance;
        }
        private Sucursales() { this.initialize(); }
        private Dictionary<int, Sucursal> sucursales = new Dictionary<int, Sucursal>();
        protected override String listQuery()
        {
            return "SELECT Provincia, Tipo, Direccion, Telefono FROM " + ClaseSQL.tableName("Sucursales");
        }

        protected override void parseResult(SqlDataReader reader)
        {
            byte provincia = reader.GetByte(0);
            byte tipo = reader.GetByte(1);
            String direccion = reader.GetString(2);
            String telefono = reader.GetString(3);
            Sucursal sucursal = new Sucursal(provincia, tipo, direccion, telefono);
            sucursales.Add(provincia, sucursal);
        }

        public List<Sucursal> list()
        {
            return sucursales.Values.ToList();
        }

        public Sucursal sucursal(byte provincia)
        {
            return sucursales[provincia];
        }
    }
}
