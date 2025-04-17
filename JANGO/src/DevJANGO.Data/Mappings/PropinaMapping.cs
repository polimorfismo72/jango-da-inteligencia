using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PropinaMapping : IEntityTypeConfiguration<Propina>
    {
        public void Configure(EntityTypeBuilder<Propina> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DescricaoPropina)
                .IsRequired()
                .HasColumnType("varchar(29)");

            builder.Property(c => c.AnoLetivo)
                .IsRequired()
               .HasColumnType("varchar(9)");

            builder.Property(p => p.PrecoPropina)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            // 1 : N => PagamentoPropina : Propinas
            //builder.HasOne(c => c.PagamentoPropina)
            //    .WithMany(c => c.Propinas)
            //.HasForeignKey(e => e.PagamentoPropinaId);

            builder.ToTable("Propinas");
        }
    }
}