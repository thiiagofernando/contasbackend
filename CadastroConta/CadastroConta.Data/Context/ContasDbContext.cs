using CadastroConta.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CadastroConta.Data.Context
{
    public class ContasDbContext : DbContext
    {
        public ContasDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Conta> conta { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                  .SelectMany(e => e.GetProperties()
                      .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContasDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
