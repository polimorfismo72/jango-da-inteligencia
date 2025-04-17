using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nota)
                .IsRequired()
                .HasColumnType("decimal(2.2)");
            
            builder.Property(c => c.AnoLetivo)
                .IsRequired()
               .HasColumnType("varchar(9)");

            builder.ToTable("Avaliacaos");
        }
    }
}