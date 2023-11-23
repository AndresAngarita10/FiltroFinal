 
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class GamaProductoConfiguration : IEntityTypeConfiguration<GamaProducto>
{
    public void Configure(EntityTypeBuilder<GamaProducto> builder)
    {
        builder.ToTable("gama_producto");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
        .HasColumnName("gama")
        .IsRequired();

        builder.Property(d => d.DescripcionTexto)
        .HasColumnName("descripcion_texto")
        .HasColumnType("text")
        .HasMaxLength(250);

        builder.Property(d => d.DescripcioHtml)
        .HasColumnName("descripcion_html")
        .HasColumnType("text")
        .HasMaxLength(250);
        
        builder.Property(d => d.Imagen)
        .HasColumnName("imagen")
        .HasColumnType("varchar")
        .HasMaxLength(250);

    }
}
