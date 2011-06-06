using System;

namespace VentaElectrodomesticos.Utils
{
    class SQLQueryBuilder
    {

        private String sqlQuery = "";

        public void select(String selectStatement)
        {
            this.add("SELECT", selectStatement);
        }

        public void from(String fromStatement)
        {
            this.add("FROM", fromStatement);
        }

        public void where(String whereStatement)
        {
            this.add("WHERE", whereStatement);
        }

        public void groupBy(String groupByStatement)
        {
            this.add("GROUP BY", groupByStatement);
        }

        public void having(String havingStatement)
        {
            this.add("HAVING", havingStatement);
        }

        public void orderBy(String orderByStatement)
        {
            this.add("ORDER BY", orderByStatement);
        }

        /*
         * TODO: El add deberia sanitizar el statement
         */
        private void add(String clauseName, String statement)
        {
            if (sqlQuery.Length > 0)
                sqlQuery += Environment.NewLine;
            if(statement.Length > 0)
                sqlQuery += clauseName + " " + statement;
        }

        public override String ToString()
        {
            return sqlQuery;
        }

    }

}