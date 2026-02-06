namespace Jornada.Models;

public class DepoimentoFoto
{
    public int DepoimentoId { get; set; }
    public Depoimento Depoimento { get; set; }

    public int FotoId { get; set; }
    public Foto Foto { get; set; }
}