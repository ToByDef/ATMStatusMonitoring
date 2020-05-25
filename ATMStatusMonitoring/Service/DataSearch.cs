using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace ATMStatusMonitoring
{
    internal static class DataSearch
    {
        internal static List<ATM> SearchATM(string search)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                return connection.Query<ATM>("SELECT * FROM ATM WHERE CONCAT (Number, LastNumber, NewNumber, SerialNumber, IP, Mask, Gateway, Address) LIKE '%" + search + "%'").ToList();
            }
        }
    }
}
