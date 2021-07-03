using CadastroConta.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroConta.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Login).IsRequired();
            builder.Property(p => p.NomeCompleto).IsRequired();
            builder.Property(p => p.Senha).IsRequired();
            builder.ToTable("Usuario");
        }
    }
}
