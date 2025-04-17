using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class TrimestreMapping : IEntityTypeConfiguration<Trimestre>
    {
        public void Configure(EntityTypeBuilder<Trimestre> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(13)");

            // 1 : N => Trimestre : Avaliacaos
            builder.HasMany(f => f.Avaliacaos)
                .WithOne(p => p.Trimestre)
                .HasForeignKey(p => p.TrimestreId);

            builder.ToTable("Trimestres");
        }
    }
}