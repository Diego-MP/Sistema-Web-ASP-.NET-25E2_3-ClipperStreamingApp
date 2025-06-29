namespace ClipperStreamingApp.Domain.Playlist.Repository;
    
public interface IPlaylistRepository
{
    Task<Playlist?> GetByIdWithItemsAsync(int id);
    void Update(Playlist playlist);

    Task<Musica?> GetMusicaByIdAsync(int id);
    Task<Banda?> GetBandaByIdAsync(int id);
     Task<int> SaveChangesAsync();
}