using Jornada.Data.Mappings;
using Jornada.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Data;

public class JornadaDataContext : DbContext
{
    public JornadaDataContext(DbContextOptions<JornadaDataContext> options)
        :base(options)
    {
        
    }

    public DbSet<Depoimento> Depoimentos { get; set; }
    public DbSet<DepoimentoFoto> DepoimentoFotos { get; set; }
    public DbSet<Destino> Destinos { get; set; }
    public DbSet<DestinoFoto> DestinoFotos { get; set; }
    public DbSet<Foto> Fotos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new DepoimentoMap());
        modelBuilder.ApplyConfiguration(new DestinoMap());
        modelBuilder.ApplyConfiguration(new DepoimentoFotoMap());
        modelBuilder.ApplyConfiguration(new DestinoFotoMap());
    }
}