using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevJANGO.Business.Models;

namespace DevJANGO.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {

        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(32)");


            // 1 : N => Curso : AlunoMatriculados
            builder.HasMany(a => a.AlunoMatriculados)
                    .WithOne(n => n.Curso)
                    .HasForeignKey(n => n.CursoId);

            // 1 : N => Curso : Classes
            builder.HasMany(a => a.Classes)
                    .WithOne(n => n.Curso)
                    .HasForeignKey(n => n.CursoId);

            builder.ToTable("Cursos");
        }

    }
}