namespace Jornada.Models;

public class DestinoFoto
{
    public int DestinoId { get; set; }
    public Destino Destino { get; set; }

    public int FotoId { get; set; }
    public Foto Foto { get; set; }
}