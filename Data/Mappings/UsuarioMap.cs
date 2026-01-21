using Jornada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jornada.Data.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{

    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
    
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.SenhaHash)
            .IsRequired()
            .HasColumnName("SenhaHash")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        
        builder.HasMany(x => x.Depoimentos)
            .WithOne(x => x.Usuario)
            .HasConstraintName("FK_Usuario_Depoimento")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
                "UsuarioRole",
                role => role.HasOne<Role>()
                            .WithMany()
                            .HasForeignKey("RoleId")
                            .HasConstraintName("FK_UsuarioRole_RoleId")
                            .OnDelete(DeleteBehavior.NoAction),
                usuario => usuario.HasOne<Usuario>()
                            .WithMany()
                            .HasForeignKey("UsuarioId")
                            .HasConstraintName("FK_UsuarioRole_UsuarioId")
                            .OnDelete(DeleteBehavior.Cascade)
            );

        builder.HasIndex(x => x.Email, "IX_Usuario_Email")
            .IsUnique();

    }
}