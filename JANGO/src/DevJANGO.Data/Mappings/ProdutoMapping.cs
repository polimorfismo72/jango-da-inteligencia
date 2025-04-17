using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");
            //-------------------------------------------
            builder.Property(p => p.ValorVenda)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            //builder.Property(m => m.ValorVenda).HasPrecision(10, 2);
            //builder.Property(a => a.Nome).HasColumnType("Name");
            //builder.Property(o => o.ValorVenda).HasColumnType("decimal(5,3)");

            // 1 : N => Produto : PedidoItens
            builder.HasMany(p => p.PedidoItems)
                .WithOne(pi => pi.Produto)
                .HasForeignKey(pi => pi.ProdutoId);

            builder.ToTable("Produtos");
        }
    }
}
