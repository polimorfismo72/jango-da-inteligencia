using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevJANGO.Business.DomainException;

namespace DevJANGO.Data.Mappings
{
    public class EncarregadoMapping : IEntityTypeConfiguration<Encarregado>
    {
        public void Configure(EntityTypeBuilder<Encarregado> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(f => f.Telefone)
                .IsRequired()
                .HasColumnType("varchar(9)");

            builder.Property(f => f.Proficao)
              .IsRequired()
              .HasColumnType("varchar(25)");

            builder.ToTable("Encarregados");
        }
        
    }
}