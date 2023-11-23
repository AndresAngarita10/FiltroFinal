 
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("producto");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
        .HasColumnName("codigo_producto")
        .IsRequired();

        builder.Property(d => d.Nombre)
        .HasColumnName("nombre")
        .HasColumnType("varchar")
        .IsRequired()
        .HasMaxLength(250);

        builder.Property(d => d.Dimensiones)
        .HasColumnName("dimensiones")
        .HasColumnType("varchar")
        .HasMaxLength(250);

        builder.Property(d => d.Proveedor)
        .HasColumnName("proveedor")
        .HasColumnType("varchar")
        .HasMaxLength(250);

        builder.Property(d => d.Descripcion)
        .HasColumnName("descripcion")
        .HasColumnType("text")
        .HasMaxLength(250);

        builder.Property(d => d.CantidadEnStok)
        .HasColumnName("cantidad_en_stock")
        .HasColumnType("smallint")
        .HasAnnotation("MySql:ColumnType", "SMALLINT(6)")
        .IsRequired();

        builder.Property(d => d.PrecioVenta)
        .HasColumnName("precio_venta")
        .HasColumnType("decimal(15,2)")
        .IsRequired();

        builder.Property(d => d.PrecioProveedor)
        .HasColumnName("precio_proveedor")
        .HasColumnType("decimal(15,2)");

        builder.HasOne(d => d.GamaProducto)
        .WithMany(d => d.Productos)
        .HasForeignKey(d => d.GamaIdFk);
    }
}
/*
API:
AspNetCoreRateLimit
AutoMapper.Extensions.Microsoft.DependencyInjection
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Mvc.Versioning
Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore.Design
System.IdentityModel.Tokens.Jwt

DOMINIO:
FluentValidation.AspNetCore
itext7.pdfhtml
Microsoft.EntityFrameworkCore

PERSISTENCIA:
Microsoft.EntityFrameworkCore
Pomelo.EntityFrameworkCore.MySql
*/
/*dotnet ef database update --project ./Persistencia/ --startup-project ./API/
 */