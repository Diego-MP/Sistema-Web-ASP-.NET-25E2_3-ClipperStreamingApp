
namespace ClipperStreamingApp.Domain.Musica.Repository;

public interface IMusicaRepository
{
    Task<IEnumerable<Playlist.Musica>> SearchByNameAsync(string term);
}