using System.Configuration;

namespace ATMStatusMonitoring
{
    internal static class Connection
    {
        internal static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
