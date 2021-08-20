using CadastroConta.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroConta.Data.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<ContaModel>
    {
        public void Configure(EntityTypeBuilder<ContaModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.PagamentoRealizado).IsRequired();
            builder.Property(p => p.Valor)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(p => p.DataPagamento);
            builder.ToTable("Conta");
        }
    }
}
