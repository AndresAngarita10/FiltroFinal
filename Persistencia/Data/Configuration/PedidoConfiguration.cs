 
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedido");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
        .HasColumnName("codigo_pedido")
        .IsRequired();

        builder.Property(d => d.FechaPedido)
        .HasColumnName("fecha_pedido")
        .HasColumnType("date")
        .IsRequired();

        builder.Property(d => d.FechaEsperada)
        .HasColumnName("fecha_esperada")
        .HasColumnType("date")
        .IsRequired();

        builder.Property(d => d.FechaEntrega)
        .HasColumnName("fecha_entrega")
        .HasColumnType("date");

        builder.Property(d => d.Estado)
        .HasColumnName("estado")
        .HasColumnType("varchar")
        .HasMaxLength(15)
        .IsRequired();

        builder.Property(d => d.Comentarios)
        .HasColumnName("comentarios")
        .HasColumnType("text")
        .HasMaxLength(250);

        builder.HasOne(d => d.Cliente)
        .WithMany(d => d.Pedidos)
        .HasForeignKey(d => d.CodigoCliente);
    }
}