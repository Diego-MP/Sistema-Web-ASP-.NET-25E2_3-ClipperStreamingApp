namespace ClipperStreamingApp.Domain;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public virtual Conta Conta { get; set; }
}


