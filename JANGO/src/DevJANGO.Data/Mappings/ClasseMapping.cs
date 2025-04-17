using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class ClasseMapping : IEntityTypeConfiguration<Classe>
    {

        public void Configure(EntityTypeBuilder<Classe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c=> c.Nome)
                .IsRequired()
                .HasColumnType("varchar(10)");
            //-------------------------------------------
            builder.Property(p => p.PrecoPropina)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            // 1 : N => Classe : AlunoMatriculados
            builder.HasMany(a => a.AlunoMatriculados)
                    .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            // 1 : N => Classe : AlunoInscritos
            builder.HasMany(a => a.AlunoInscritos)
                    .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            // 1 : N => Classe : Turmas
            builder.HasMany(a => a.Turmas)
                    .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            // 1 : N => Classe : Avaliacaos
            builder.HasMany(a => a.Avaliacaos)
                    .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            // 1 : N => Classe : Propinas
            builder.HasMany(a => a.Propinas)
                    .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            // 1 : N => Classe : ProfessorDisciplinaClasses
            builder.HasMany(a => a.ProfessorDisciplinaClasses)
               .WithOne(n => n.Classe)
                    .HasForeignKey(n => n.ClasseId);

            builder.ToTable("Classes");
        }

    }
}
