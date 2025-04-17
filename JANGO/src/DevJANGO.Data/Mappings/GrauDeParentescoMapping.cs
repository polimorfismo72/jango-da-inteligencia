using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevJANGO.Business.Models;

namespace DevJANGO.Data.Mappings
{
    public class GrauDeParentescoMapping : IEntityTypeConfiguration<GrauDeParentesco>
    {

        public void Configure(EntityTypeBuilder<GrauDeParentesco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeGrauParentesco)
                .IsRequired()
                .HasColumnType("varchar(60)");

          

            // 1 : N => Mes : AlunoMatriculados
            builder.HasMany(a => a.AlunoMatriculados)
                    .WithOne(n => n.GrauDeParentesco)
                    .HasForeignKey(n => n.GrauDeParentescoId);

            // 1 : N => Mes : AlunoInscritos
            builder.HasMany(a => a.AlunoInscritos)
                    .WithOne(n => n.GrauDeParentesco)
                    .HasForeignKey(n => n.GrauDeParentescoId);
           


            builder.ToTable("GrauDeParentescos");
        }

    }
}