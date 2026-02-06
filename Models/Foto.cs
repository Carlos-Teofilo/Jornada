namespace Jornada.Models;

public class Foto
{
    public int Id { get; set; }
    public string Url { get; set; }

    public IList<DestinoFoto> DestinoFotos { get; set; }
    public IList<DepoimentoFoto> DepoimentoFotos { get; set; }
}