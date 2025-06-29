namespace ClipperStreamingApp.Domain;

public class Conta
{
    public int Id { get; set; }
    public virtual Usuario Usuario { get; set; }
    
    public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>(); 
    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>(); 

    
}