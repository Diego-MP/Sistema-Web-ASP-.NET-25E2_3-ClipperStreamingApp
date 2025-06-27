namespace ClipperStreamingApp.Domain;

public class Playlist
{
    public int Id { get; set; }
    public Conta Conta { get; set; }
    public List<Musica> Musicas { get; set; }
    public List<Banda> Bandas { get; set; }
}