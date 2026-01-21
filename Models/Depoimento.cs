namespace Jornada.Models;

public class Depoimento
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public string Foto { get; set; }

    public Usuario Usuario { get; set; }

}