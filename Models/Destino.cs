namespace Jornada.Models;

public class Destino
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Preco { get; set; }
    public string Meta { get; set; }
    public string TextoDescritivo { get; set; }

    public IList<DestinoFoto> DestinoFotos { get; set; }
}
