using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class AlunoInscritoMapping : IEntityTypeConfiguration<AlunoInscrito>
    {
        public void Configure(EntityTypeBuilder<AlunoInscrito> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
               .HasDefaultValueSql("NEXT VALUE FOR MinhaSequenciaCodigoAluno");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.NomeDoPai)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.NomeDaMae)
               .IsRequired()
               .HasColumnType("varchar(60)");

            builder.Property(c => c.Datanascimento)
              .IsRequired()
              .HasColumnType("datetime");

            builder.Property(p => p.Imagem)
              .IsRequired()
              .HasColumnType("varchar(250)");

            builder.Property(p => p.NumDocumento)
              .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(p => p.EscolaDeOrgigem)
           .IsRequired()
           .HasColumnType("varchar(60)");

            builder.Property(c => c.AnoLetivo)
               .IsRequired()
              .HasColumnType("varchar(9)");

            builder.Property(p => p.Endereco)
            .IsRequired()
            .HasColumnType("varchar(250)");
            //-------------------------------------------
            builder.Property(p => p.ValorDaInscricao)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            // 1 : N => AlunoInscrito  : AlunoMatriculados 
            builder.HasMany(a => a.AlunoMatriculados)
                .WithOne(av => av.AlunoInscrito)
                .HasForeignKey(av => av.AlunoInscritoId);

            builder.ToTable("AlunoInscritos");
        }
    }
}