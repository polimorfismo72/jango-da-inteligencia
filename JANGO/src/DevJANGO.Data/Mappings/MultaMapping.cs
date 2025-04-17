using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class MultaMapping : IEntityTypeConfiguration<Multa>
    {
        public void Configure(EntityTypeBuilder<Multa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DescricaoMulta)
                .IsRequired()
                .HasColumnType("varchar(29)");

            builder.Property(c => c.AnoLetivo)
                .IsRequired()
                .HasColumnType("varchar(9)");
            //-------------------------------------------
            builder.Property(p => p.PrecoPropina)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.ToTable("Multas");
        }
    }
}