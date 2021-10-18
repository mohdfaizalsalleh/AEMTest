using DBModel.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace DBModel
{
    public partial class AEMContext : DbContext
    {
        public AEMContext()
        {
        }

        public AEMContext(DbContextOptions<AEMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlatformInfo> PlatformInfo { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AEMTest;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
