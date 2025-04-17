using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevJANGO.Business.Models;

namespace DevJANGO.Data.Mappings
{
    public class AreaDeConhecimentoMapping : IEntityTypeConfiguration<AreaDeConhecimento>
    {
        public void Configure(EntityTypeBuilder<AreaDeConhecimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(10)");

            // 1 : N => AreaDeConhecimento : AlunoInscritos
            builder.HasMany(a => a.AlunoInscritos)
                    .WithOne(n => n.AreaDeConhecimento)
                    .HasForeignKey(n => n.AreaDeConhecimentoId);

            // 1 : N => Classe : Turmas
            builder.HasMany(a => a.Turmas)
                     .WithOne(n => n.AreaDeConhecimento)
                    .HasForeignKey(n => n.AreaDeConhecimentoId);

            builder.ToTable("AreaDeConhecimentos");
        }
    }
}