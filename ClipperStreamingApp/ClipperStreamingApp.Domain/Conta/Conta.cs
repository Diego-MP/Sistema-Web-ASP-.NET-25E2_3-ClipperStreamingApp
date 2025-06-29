namespace ClipperStreamingApp.Domain.Conta;

public class Conta
{
    public int Id { get; set; }
    public virtual Usuario Usuario { get; set; }
    
    public virtual ICollection<Assinatura.Assinatura> Assinaturas { get; set; } = new List<Assinatura.Assinatura>(); 
    public virtual ICollection<Playlist.Playlist> Playlists { get; set; } = new List<Playlist.Playlist>(); 

    
}