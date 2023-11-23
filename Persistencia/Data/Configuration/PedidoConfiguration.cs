using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PedidoConfiguration : IEntityTypeConfiguration<Pedido> 
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {

        builder.ToTable("Pedido");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.FechaPedido)
        .HasColumnName("FechaPedido")
        .HasColumnType("Date");

        builder.Property(p => p.FechaEntregada)
        .HasColumnName("FechaEntregada")
        .HasColumnType("Date");

        builder.Property(p => p.FechaEsperada)
        .HasColumnName("FechaEsperada")
        .HasColumnType("Date");

        builder.Property(p => p.Estado)
        .HasColumnName("Estado")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Comentarios)
        .HasColumnName("Comentarios")
        .HasColumnType("varchar")
        .HasMaxLength(255);

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Pedidos)
        .HasForeignKey(p => p.IdClienteFk);

    }
}