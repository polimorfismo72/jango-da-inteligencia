using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class AlunoMatriculadoMapping : IEntityTypeConfiguration<AlunoMatriculado>
    {
        public void Configure(EntityTypeBuilder<AlunoMatriculado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(p => p.NumDocumento)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(p => p.Imagem)
              .IsRequired()
              .HasColumnType("varchar(250)");

            builder.Property(c => c.AnoLetivo)
                .IsRequired()
               .HasColumnType("varchar(9)");
            //-------------------------------------------
            builder.Property(p => p.ValorDaMatricula)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            // 1 : N => AlunoMatriculado : Avaliacao
            builder.HasMany(a => a.Avaliacaos)
                .WithOne(av => av.AlunoMatriculado)
                .HasForeignKey(av => av.AlunoMatriculadoId);

            // 1 : N => AlunoMatriculado : PagamentoPropina
            builder.HasMany(a => a.PagamentoPropinas)
                .WithOne(p => p.AlunoMatriculado)
                .HasForeignKey(p => p.AlunoMatriculadoId);

            // 1 : N => AlunoMatriculado : PagamentoMultas
            builder.HasMany(a => a.PagamentoMultas)
                .WithOne(p => p.AlunoMatriculado)
                .HasForeignKey(p => p.AlunoMatriculadoId);

            // 1 : N => AlunoMatriculado : Propina
            builder.HasMany(a => a.Propinas)
                .WithOne(p => p.AlunoMatriculado)
                .HasForeignKey(p => p.AlunoMatriculadoId);

            // 1 : N => AlunoMatriculado : Multa
            builder.HasMany(a => a.Multas)
                .WithOne(m => m.AlunoMatriculado)
                .HasForeignKey(m => m.AlunoMatriculadoId);

            builder.ToTable("AlunoMatriculados");
        }
    }
}