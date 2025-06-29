namespace ClipperStreamingApp.Domain.Playlist.Repository;

public interface IBandaRepository
{
    Task<Banda?> GetByIdWithMusicasAsync(int id);
    Task<IEnumerable<Banda>> SearchByNameAsync(string term);

}