using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevJANGO.Business.Models;

namespace DevJANGO.Data.Mappings
{
    public class MesMapping : IEntityTypeConfiguration<Mes>
    {

        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeMes)
                .IsRequired()
                .HasColumnType("varchar(9)");

            // 1 : N => Mes : PagamentoPropinas
            //builder.HasMany(a => a.PagamentoPropinas)
            //        .WithOne(n => n.Mes)
            //        .HasForeignKey(n => n.MesId);

            // 1 : N => Mes : Propinas
            builder.HasMany(a => a.Propinas)
                    .WithOne(n => n.Mes)
                    .HasForeignKey(n => n.MesId);
            // 1 : N => Mes : Multas
            builder.HasMany(a => a.Multas)
                    .WithOne(n => n.Mes)
                    .HasForeignKey(n => n.MesId);


            builder.ToTable("Meses");
        }

    }
}