using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeDisciplina)
                .IsRequired()
                .HasColumnType("varchar(45)");

            // 1 : N => Disciplina : Avaliacaos
            builder.HasMany(a => a.Avaliacaos)
                    .WithOne(n => n.Disciplina)
                    .HasForeignKey(n => n.DisciplinaId);

            // 1 : N => Disciplina : ProfessorDisciplinaClasses
            builder.HasMany(a => a.ProfessorDisciplinaClasses)
                .WithOne(n => n.Disciplina)
                    .HasForeignKey(n => n.DisciplinaId);

            builder.ToTable("Disciplinas");
        }
    }
}