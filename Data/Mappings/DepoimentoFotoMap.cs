using Jornada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jornada.Data.Mappings;

public class DepoimentoFotoMap : IEntityTypeConfiguration<DepoimentoFoto>
{
    public void Configure(EntityTypeBuilder<DepoimentoFoto> builder)
    {
        builder.ToTable("Depoimento_Foto");

        builder.HasKey(x => new { x.DepoimentoId, x.FotoId });

        builder.HasOne(x => x.Depoimento)
            .WithMany(x => x.DepoimentoFotos)
            .HasForeignKey(x => x.DepoimentoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Foto)
            .WithMany(x => x.DepoimentoFotos)
            .HasForeignKey(x => x.FotoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}