using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using user_changes_domain.Data.Entities;

namespace user_changes_domain.Data.Mappings
{
    public class TituloMapping : IEntityTypeConfiguration<Titulo>
    {
        public void Configure(EntityTypeBuilder<Titulo> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.codEspecieDoc)
                .IsRequired()
                .HasColumnName("codEspecieDoc")
                .HasColumnType("varchar(20)");
            
            builder.Property(c => c.idLinha)
                .IsRequired()
                .HasColumnName("idLinha")
                .HasColumnType("varchar(20)");
            
            builder.Property(c => c.seuNumero)
                .IsRequired()
                .HasColumnName("seuNumero")
                .HasColumnType("varchar(20)");

            builder.Property(c => c.dataVencimento)
                .IsRequired()
                .HasColumnName("dataVencimento")
                .HasColumnType("datetime2");

            builder.ToTable("Titulo");

        }
    }
}
