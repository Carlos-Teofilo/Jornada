using Jornada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jornada.Data.Mappings;

public class DestinoMap : IEntityTypeConfiguration<Destino>
{
    public void Configure(EntityTypeBuilder<Destino> builder)
    {
        builder.ToTable("Destino");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Preco)
            .IsRequired()
            .HasColumnName("Preco")
            .HasColumnType("INT");

        builder.Property(x => x.Foto)
            .IsRequired(false)
            .HasColumnName("Foto")
            .HasColumnType("NVARCHAR");


        builder.ToTable(x => x.HasCheckConstraint("CK_Preco_Greather_Than_Zero", "[Preco] > 0"));
    }
}