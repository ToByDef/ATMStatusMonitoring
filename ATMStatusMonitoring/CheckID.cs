using System.Data;
using Dapper;

namespace ATMStatusMonitoring
{
    class CheckID
    {
        public bool CheckIDStatus(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                IDataReader dataReader = connection.ExecuteReader("dbo.CheckIDStatus " + id);
                if (dataReader.Read())
                    return true;
                else
                    return false;
            }
        }

        public int CheckIDATM(string name)  // returns 0 if there is no record in ATM or record ID, if any
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                try
                {
                    IDataReader dataReader = connection.ExecuteReader("dbo.CheckIDATM " + name);
                    if (dataReader.Read())
                        return dataReader.GetInt32(0);
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int CheckNewNumber(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                try
                {
                    IDataReader dataReader = connection.ExecuteReader("dbo.CheckNewNumber " + name);
                    if (dataReader.Read())
                        return dataReader.GetInt32(0);
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int CheckLastNumber(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                try
                {
                    IDataReader dataReader = connection.ExecuteReader("dbo.CheckLastNumber " + name);
                    if (dataReader.Read())
                        return dataReader.GetInt32(0);
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
