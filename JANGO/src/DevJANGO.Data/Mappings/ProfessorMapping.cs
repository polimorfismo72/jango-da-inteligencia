using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class ProfessorMapping : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");
            

            builder.Property(c => c.BI)
                .IsRequired()
               .HasColumnType("varchar(16)");

            builder.Property(c => c.Telefone)
             .IsRequired()
            .HasColumnType("varchar(9)");

            builder.Property(c => c.Email)
            .IsRequired()
           .HasColumnType("varchar(254)");

            builder.Property(c => c.Endereco)
          .IsRequired()
         .HasColumnType("varchar(250)");

           

            // 1 : N => Professor : Propinas
            builder.HasMany(a => a.Avaliacaos)
                    .WithOne(n => n.Professor)
                    .HasForeignKey(n => n.ProfessorId);

            // 1 : N => Professor : ProfessorDisciplinaClasses
            builder.HasMany(a => a.ProfessorDisciplinaClasses)
                  .WithOne(n => n.Professor)
                    .HasForeignKey(n => n.ProfessorId);

            builder.ToTable("Professores");
        }
    }
}