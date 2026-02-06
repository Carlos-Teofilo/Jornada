using Jornada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DestinoFotoMap : IEntityTypeConfiguration<DestinoFoto>
{
    public void Configure(EntityTypeBuilder<DestinoFoto> builder)
    {
        builder.ToTable("Destino_Foto");

        builder.HasKey(x => new { x.DestinoId, x.FotoId});

        builder.HasOne(x => x.Destino)
            .WithMany(x => x.DestinoFotos)
            .HasForeignKey(x => x.DestinoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Foto)
            .WithMany(x => x.DestinoFotos)
            .HasForeignKey(x => x.FotoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}