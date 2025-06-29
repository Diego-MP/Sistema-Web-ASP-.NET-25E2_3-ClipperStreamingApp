namespace ClipperStreamingApp.Domain.Playlist;

public class Banda
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();

}