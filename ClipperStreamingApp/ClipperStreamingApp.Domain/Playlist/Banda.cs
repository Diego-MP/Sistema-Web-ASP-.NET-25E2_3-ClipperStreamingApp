namespace ClipperStreamingApp.Domain;

public class Banda
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();

}