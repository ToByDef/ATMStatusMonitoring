using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStatusMonitoring
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("ATMDb")
        {
        }

        public DbSet<ATM> ATMs { get; set; }
        public DbSet<ATMStatus> ATMStatuses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
