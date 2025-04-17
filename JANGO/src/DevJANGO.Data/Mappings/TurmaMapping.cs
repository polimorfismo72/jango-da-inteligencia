using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeTurma)
                .IsRequired()
                .HasColumnType("varchar(10)");

            // 1 : N => Turma : AlunoMatriculados
            builder.HasMany(f => f.AlunoMatriculados)
                .WithOne(p => p.Turma)
                .HasForeignKey(p => p.TurmaId);


            // 1 : N => Turma : Avaliacaos
            builder.HasMany(f => f.Avaliacaos)
                .WithOne(p => p.Turma)
                .HasForeignKey(p => p.TurmaId);

            // 1 : N => Turma : Propinas
            builder.HasMany(f => f.Propinas)
                .WithOne(p => p.Turma)
                .HasForeignKey(p => p.TurmaId);

            builder.ToTable("Turmas");
        }
    }
}