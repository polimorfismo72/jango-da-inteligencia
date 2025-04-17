using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class ProfessorDisciplinaClasseMapping : IEntityTypeConfiguration<ProfessorDisciplinaClasse>
    {

        public void Configure(EntityTypeBuilder<ProfessorDisciplinaClasse> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeDisciplina)
                .IsRequired()
                .HasColumnType("varchar(45)");

            builder.Property(c => c.NomeClasse)
              .IsRequired()
              .HasColumnType("varchar(10)");

            builder.Property(c => c.AnoLetivo)
              .IsRequired()
             .HasColumnType("varchar(9)");

            builder.ToTable("ProfessorDisciplinaClasses");
        }

    }
}
