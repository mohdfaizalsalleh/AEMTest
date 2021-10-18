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
                //optionsBuilder.UseSqlServer("Server=localhost;user=sa;password=123456;Database=AEMTest");
                //optionsBuilder.UseSqlServer("Server=10.254.1.45;user=ewartadba;password=eW@rtA2018;Database=eWartaBK");
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
