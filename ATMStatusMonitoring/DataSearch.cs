using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ATMStatusMonitoring
{
    class DataSearch
    {
        public List<ATM> SearchATM(string search)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                return connection.Query<ATM>("SELECT * FROM ATM WHERE CONCAT (Number, LastNumber, NewNumber, SerialNumber, IP, Mask, Gateway, Address) LIKE '%" + search + "%'").ToList();
            }
        }
    }
}
