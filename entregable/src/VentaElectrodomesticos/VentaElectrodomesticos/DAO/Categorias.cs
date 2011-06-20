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
    class Categorias
    {
        public static Categorias getInstance()
        {
            return new Categorias();
        }

        private DataTable table()
        {
            String query = "SELECT Codigo, Nombre, Padre FROM " + ClaseSQL.tableName("Categorias");
            return ClaseSQL.getInstance().selectDataTable(query);
        }

        //TODO: no es muy feliz que un DAO conozca elementos de la GUI
        public void fillTree(TreeView treeView)
        {
            this.parsearNivel(null, "Padre is null", this.table(), treeView);
        }

        private void parsearNivel(string parentid, string filter, DataTable table, TreeView treeView)
        {
            DataRow[] foundRows = table.Select(filter, "Nombre");

            if (foundRows.Length == 0)
                return;

            // Get TreeNode of parent using Find which looks in the name
            // property of each node. true itterates all children
            TreeNode[] parentNode = treeView.Nodes.Find(parentid, true);
            if (parentid != null)
                if (parentNode.Length == 0)
                    return;

            // Add each row to tree
            for (int i = 0; i <= foundRows.GetUpperBound(0); i++)
            {
                string nodetext = foundRows[i]["Nombre"].ToString();
                string nodeid = foundRows[i]["Codigo"].ToString();

                TreeNode node = new TreeNode();
                node.Text = nodetext;
                node.Name = nodeid; // This is critical as the Find method searches the Name property

                if (parentid == null)
                    treeView.Nodes.Add(node); // Top Level
                else
                    parentNode[0].Nodes.Add(node); // Add children under parent

                // Itterate into any nodes
                parsearNivel(nodeid, "Padre = " + nodeid, table, treeView);
            }
        }
    }
}
