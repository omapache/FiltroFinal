using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ProductoConfiguration : IEntityTypeConfiguration<Producto> 
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {

        builder.ToTable("Producto");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.GamaProducto)
        .WithMany(p => p.Productos)
        .HasForeignKey(p => p.IdGamaProductoFk);

        builder.Property(p => p.Dimension)
        .HasColumnName("Dimension")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Proveedor)
        .HasColumnName("Proveedor")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Descripcion)
        .HasColumnName("Descripcion")
        .HasColumnType("text")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.CantidadStock)
        .HasColumnName("CantidadStock")
        .HasColumnType("smallint")
        .IsRequired();

        builder.Property(p => p.PrecioProveedor)
        .HasColumnName("PrecioProveedor")
        .HasColumnType("decimal(18,10)")
        .IsRequired();

        builder.Property(p => p.PrecioVenta)
        .HasColumnName("PrecioVenta")
        .HasColumnType("decimal(18,10)")
        .IsRequired();

    }
}