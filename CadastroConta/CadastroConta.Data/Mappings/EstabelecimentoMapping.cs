using CadastroConta.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroConta.Data.Mappings
{
    public class EstabelecimentoMapping : IEntityTypeConfiguration<EstabelecimentoModel>
    {
        public void Configure(EntityTypeBuilder<EstabelecimentoModel> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Descricao)
                .IsRequired().HasColumnType("varchar(100)");
            builder.ToTable("Estabelecimento");
        }
    }
}