namespace Jornada.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string SenhaHash { get; set; }

    public IList<Depoimento> Depoimentos { get; set; }
    public IList<Role> Roles { get; set; }
}