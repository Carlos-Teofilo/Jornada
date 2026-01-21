using Jornada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jornada.Data.Mappings;

public class DepoimentoMap : IEntityTypeConfiguration<Depoimento>
{
    public void Configure(EntityTypeBuilder<Depoimento> builder)
    {
        //Tabela
        builder.ToTable("Depoimento");

        // Id
        builder.HasKey(x => x.Id);

        //Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Foto)
            .HasColumnName("Foto")
            .HasColumnType("NVARCHAR")
            .IsRequired(false);

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Depoimentos)
            .HasConstraintName("FK_Depoimento_Usuario")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}