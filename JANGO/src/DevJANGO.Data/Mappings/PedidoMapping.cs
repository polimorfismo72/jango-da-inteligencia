using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {

        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(n => n.Codigo)
               .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.Property(p => p.ValorDesconto)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

            builder.Property(p => p.ValorTotal)
               .IsRequired()
               .HasColumnType("decimal(10,2)");
            //builder.Property(p => p.NumeroDeTransacaoDePagamento)
            // .IsRequired()
            // .HasColumnType("varchar(150)");

            // 1 : N => Pedido : PedidoItem
            builder.HasMany(p => p.PedidoItems)
                .WithOne(pi => pi.Pedido)
                .HasForeignKey(pi => pi.PedidoId);


            builder.ToTable("Pedidos");
        }


    }
}