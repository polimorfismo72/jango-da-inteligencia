using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class AplicaMultaMapping : IEntityTypeConfiguration<AplicaMulta>
    {
        public void Configure(EntityTypeBuilder<AplicaMulta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.ToTable("AplicaMultas");
        }
    }
}