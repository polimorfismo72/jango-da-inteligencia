using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class FuncionarioCaixaMapping : IEntityTypeConfiguration<FuncionarioCaixa>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCaixa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(254)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");

            // 1 : N => FuncionarioCaixa : PagamentoPropinaItems
            builder.HasMany(a => a.PagamentoPropinas)
                    .WithOne(n => n.FuncionarioCaixa)
                    .HasForeignKey(n => n.FuncionarioCaixaId);

            // 1 : N => FuncionarioCaixa : PagamentoMultaItems
            builder.HasMany(a => a.PagamentoMultaItems)
                    .WithOne(n => n.FuncionarioCaixa)
                    .HasForeignKey(n => n.FuncionarioCaixaId);

            // 1 : N => FuncionarioCaixa : AlunoInscritos
            builder.HasMany(a => a.AlunoInscritos)
                    .WithOne(n => n.FuncionarioCaixa)
                    .HasForeignKey(n => n.FuncionarioCaixaId);

            // 1 : N => FuncionarioCaixa : AlunoMatriculados
            builder.HasMany(a => a.AlunoMatriculados)
                    .WithOne(n => n.FuncionarioCaixa)
                    .HasForeignKey(n => n.FuncionarioCaixaId);

            // 1 : N => Vendedores : PedidoItem
            builder.HasMany(f => f.PedidoItems)
                .WithOne(p => p.FuncionarioCaixa)
                .HasForeignKey(p => p.FuncionarioCaixaId);

            builder.ToTable("FuncionarioCaixas");
        }
    }
}