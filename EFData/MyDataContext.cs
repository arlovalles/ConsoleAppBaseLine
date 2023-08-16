using Microsoft.EntityFrameworkCore;
using EFData.model;

namespace EFData
{
    public class MyDataContext:DbContext 
    {
        DbSet<MyType> MyTypes { get; set; }   

        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}