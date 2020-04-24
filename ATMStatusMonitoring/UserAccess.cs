using System.Data;
using System.Linq;
using Dapper;

namespace ATMStatusMonitoring
{
    class UserAccess
    {
        public User GetUser(string login, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ATMDb")))
            {
                return connection.Query<User>("dbo.GetUser @Login, @Password", new { login, password }).FirstOrDefault();
            }
        }
    }
}
