using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PagamentoMultaMapping : IEntityTypeConfiguration<PagamentoMulta>
    {
        public void Configure(EntityTypeBuilder<PagamentoMulta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
               .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");
            //-------------------------------------------
            builder.Property(p => p.PercentualDesconto)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.PercentualDesconto)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

            builder.Property(p => p.ValorDesconto)
             .IsRequired()
             .HasColumnType("decimal(10,2)");

            builder.Property(p => p.TotalPago)
             .IsRequired()
             .HasColumnType("decimal(10,2)");
            //builder.Property(c => c.NumeroDeTransacaoDePagamento)
            //    .IsRequired()
            //    .HasColumnType("varchar(150)");

            builder.ToTable("PagamentoMultas");
        }
    }
}