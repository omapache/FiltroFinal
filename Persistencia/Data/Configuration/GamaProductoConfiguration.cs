using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class GamaProductoConfiguration : IEntityTypeConfiguration<GamaProducto> 
{
    public void Configure(EntityTypeBuilder<GamaProducto> builder)
    {

        builder.ToTable("GamaProducto");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.DescripcionTexto)
        .HasColumnName("DescripcionTexto")
        .HasColumnType("text")
        .HasMaxLength(255)
        .IsRequired();

         builder.Property(p => p.DescripcionHtml)
        .HasColumnName("DescripcionHtml")
        .HasColumnType("text")
        .HasMaxLength(255);

         builder.Property(p => p.Imagen)
        .HasColumnName("Imagen")
        .HasColumnType("varchar")
        .HasMaxLength(256);
    }
}