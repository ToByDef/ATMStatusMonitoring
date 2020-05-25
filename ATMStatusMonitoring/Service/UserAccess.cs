using System.Data;
using System.Linq;
using Dapper;

namespace ATMStatusMonitoring
{
    internal static class UserAccess
    {
        internal static User GetUser(string login, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connection.CnnVal("ATMDb")))
            {
                return connection.Query<User>("dbo.GetUser @Login, @Password", new { login, password }).FirstOrDefault();
            }
        }
    }
}
