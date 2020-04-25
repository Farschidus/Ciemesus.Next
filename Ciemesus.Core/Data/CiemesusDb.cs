using Ciemesus.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Ciemesus.Core.Data
{
    public class CiemesusDb : DbContext
    {
        private readonly string _connectionString;

        public CiemesusDb(ConnectionStringSettings connectionStringSettings)
            : base()
        {
            _connectionString = connectionStringSettings.CiemesusDb;
        }

        public CiemesusDb(DbContextOptions<CiemesusDb> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserApplicationRole> UserApplicationRoles { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>()
                .HasKey(x => new { x.Application, x.Role });

            modelBuilder.Entity<UserApplicationRole>()
                .HasKey(x => new { x.UserId, x.Application, x.Role });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
