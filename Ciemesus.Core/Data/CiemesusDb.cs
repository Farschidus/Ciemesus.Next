using System.Data.Entity;

namespace Ciemesus.Core.Data
{
    public class CiemesusDb : DbContext
    {
        public CiemesusDb()
            : base()
        {
            Database.SetInitializer<CiemesusDb>(null);
        }

        public CiemesusDb(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<CiemesusDb>(null);
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
