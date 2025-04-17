using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevJANGO.Business.Models;

namespace DevJANGO.Data.Mappings
{
    public class NiveisDeEnsinoMapping : IEntityTypeConfiguration<NiveisDeEnsino>
    {

        public void Configure(EntityTypeBuilder<NiveisDeEnsino> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeNiveisDeEnsino)
                .IsRequired()
                .HasColumnType("varchar(10)");

            // 1 : N => NiveisDeEnsino : AlunoMatriculados
            builder.HasMany(a => a.AlunoMatriculados)
                    .WithOne(n => n.NiveisDeEnsino)
                    .HasForeignKey(n => n.NiveisDeEnsinoId);

            // 1 : N => NiveisDeEnsino : AlunoInscritos
            builder.HasMany(a => a.AlunoInscritos)
                    .WithOne(n => n.NiveisDeEnsino)
                    .HasForeignKey(n => n.NiveisDeEnsinoId);
            // 1 : N => NiveisDeEnsino : Classe
            builder.HasMany(a => a.Classes)
                    .WithOne(n => n.NiveisDeEnsino)
                    .HasForeignKey(n => n.NiveisDeEnsinoId);

            // 1 : N => NiveisDeEnsino : Disciplinas
            builder.HasMany(a => a.Disciplinas)
                    .WithOne(n => n.NiveisDeEnsino)
                    .HasForeignKey(n => n.NiveisDeEnsinoId);

            builder.ToTable("NiveisDeEnsinos");
        }

    }
}