using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PagamentoPropinaMapping : IEntityTypeConfiguration<PagamentoPropina>
    {
        public void Configure(EntityTypeBuilder<PagamentoPropina> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Codigo)
             .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");
            //-------------------------------------------
            builder.Property(p => p.PrecoPropina)
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
            //    .HasColumnType("varchar(29)");

            builder.Property(c => c.Descricao)
             .IsRequired()
             .HasColumnType("varchar(40)");

            // 1 : N => PagamentoPropna : Propina
            builder.HasMany(a => a.Propinas)
                .WithOne(av => av.PagamentoPropina)
                .HasForeignKey(av => av.PagamentoPropinaId);

            builder.ToTable("PagamentoPropinas");
        }
    }
}