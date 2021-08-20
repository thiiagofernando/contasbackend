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

        public DbSet<ContaModel> conta { get; set; }
        public DbSet<UsuarioModel> usuario { get; set; }
        public DbSet<EstabelecimentoModel> estabelecimento { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                  .SelectMany(e => e.GetProperties()
                      .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContasDbContext).Assembly);

            //impedir delete cascade no banco
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
