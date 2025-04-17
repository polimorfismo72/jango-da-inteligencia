using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NomeProduto)
             .IsRequired()
             .HasColumnType("varchar(250)");
            //-------------------------------------------
            builder.Property(p => p.ValorUnitario)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            // 1 : N => Pedido : PedidoItens
            builder.HasOne(c => c.Pedido)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.PedidoId);

            // 1 : N => Produto : PedidoItens
            builder.HasOne(c => c.Produto)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.ProdutoId);

            // 1 : N => Vendedor : PedidoItens
            builder.HasOne(c => c.FuncionarioCaixa)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.FuncionarioCaixaId);

            builder.ToTable("PedidoItems");
        }
    }
}