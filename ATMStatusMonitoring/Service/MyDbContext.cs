using Microsoft.EntityFrameworkCore;

namespace ATMStatusMonitoring.Service
{
    class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ATMStatus> ATMStatuses { get; set; }
        public DbSet<ATM> ATMs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ATMDatabase; Trusted_Connection=True");
        }
    }
}
