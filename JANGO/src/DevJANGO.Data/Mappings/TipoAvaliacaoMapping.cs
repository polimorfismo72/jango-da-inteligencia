using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class TipoAvaliacaoMapping : IEntityTypeConfiguration<TipoAvaliacao>
    {
        public void Configure(EntityTypeBuilder<TipoAvaliacao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(18)");

            // 1 : N => TipoAvaliacao : Avaliacaos
            builder.HasMany(f => f.Avaliacaos)
                .WithOne(p => p.TipoAvaliacao)
                .HasForeignKey(p => p.TipoAvaliacaoId);

            builder.ToTable("TipoAvaliacaos");
        }
    }
}