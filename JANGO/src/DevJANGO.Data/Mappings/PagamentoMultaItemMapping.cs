using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PagamentoMultaItemMapping : IEntityTypeConfiguration<PagamentoMultaItem>
    {
        public void Configure(EntityTypeBuilder<PagamentoMultaItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeMulta)
                .IsRequired()
                .HasColumnType("varchar(29)");

            builder.ToTable("PagamentoMultaItems");
        }
    }
}