using System.Data;
using Dapper;

namespace ATMStatusMonitoring
{
    class CheckID
    {
        IDataReader dataReader;

        public bool CheckIDStatus(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                dataReader = connection.ExecuteReader("dbo.CheckIDStatus " + id);
                return dataReader.Read() ? true : false;
            }
        }

        public int CheckIDATM(string name)  // returns 0 if there is no record in ATM or record ID, if any
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                if (string.IsNullOrEmpty(name))
                    return 0;
                dataReader = connection.ExecuteReader("dbo.CheckIDATM " + name);
                return dataReader.Read() ? dataReader.GetInt32(0) : 0;
            }
        }

        public int CheckNewNumber(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                if (string.IsNullOrEmpty(name))
                    return 0;
                dataReader = connection.ExecuteReader("dbo.CheckNewNumber " + name);
                return dataReader.Read() ? dataReader.GetInt32(0) : 0;
            }
        }

        public int CheckLastNumber(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                if (string.IsNullOrEmpty(name))
                    return 0;
                dataReader = connection.ExecuteReader("dbo.CheckLastNumber " + name);
                return dataReader.Read() ? dataReader.GetInt32(0) : 0;
            }
        }
    }
}
