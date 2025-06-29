namespace ClipperStreamingApp.Domain.Playlist;


public class Playlist
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    public Conta.Conta Conta { get; set; }
    public List<Musica> Musicas { get; set; }
    public List<Banda> Bandas { get; set; }
    
    public void AdicionarMusica(Musica musica)
    {
        if (musica == null)
            throw new ArgumentNullException(nameof(musica));

        if (this.Musicas.Any(m => m.Id == musica.Id))
        {
            throw new ArgumentException("Musica j<UNK> existente");
        }
        
        if (this.Musicas.Count >= 100)
        {
           throw new Exception("playlist atingiu o limite máximo de 100 músicas.");
        }

        Musicas.Add(musica);
    }
    
    public void AdicionarMusicas(IEnumerable<Musica> musicasParaAdicionar)
    {
        if (musicasParaAdicionar == null) return;
        
        var idsDeMusicasExistentes = this.Musicas.Select(m => m.Id).ToHashSet();

        var musicasNovas = musicasParaAdicionar.Where(musica => !idsDeMusicasExistentes.Contains(musica.Id));

        foreach (var musicaNova in musicasNovas)
        {
            AdicionarMusica(musicaNova);
        }
    }
    
    public void AdicionarBanda(Banda banda)
    {
        if (banda == null)
            throw new ArgumentNullException(nameof(banda));

        if (Bandas.Any(b => b.Id == banda.Id))
        {
            throw new ArgumentException("Banda j<UNK> existente");
        }

        Bandas.Add(banda);
    }
}