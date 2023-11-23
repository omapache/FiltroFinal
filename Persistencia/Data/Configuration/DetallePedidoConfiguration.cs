using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido> 
{
    public void Configure(EntityTypeBuilder<DetallePedido> builder)
    {

        builder.ToTable("DetallePedido");
        builder.HasKey(p => new {p.IdPedidoFk, p.IdProductoFk});


        builder.HasOne(p => p.Producto)
        .WithMany(p => p.DetallePedidos)
        .HasForeignKey(p => p.IdProductoFk);

        builder.HasOne(p => p.Pedido)
        .WithMany(p => p.DetallePedidos)
        .HasForeignKey(p => p.IdPedidoFk);

        builder.Property(p => p.Cantidad)
        .HasColumnName("Cantidad")
        .HasColumnType("int")
        .IsRequired();

        builder.Property(p => p.PrecioUnidad)
        .HasColumnName("PrecioUnidad")
        .HasColumnType("decimal(18,10)")
        .IsRequired();

        builder.Property(p => p.NumeroSalida)
        .HasColumnName("NumeroSalida")
        .HasColumnType("smallint")
        .IsRequired();

    }
}