using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.WebApp.Models
{
    public class BandaSearchResultViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    public class MusicaSearchResultViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeBanda { get; set; }
    }
    
    public class SearchViewModel
    {
        [Display(Name = "Pesquisar por Músicas ou Bandas")]
        public string Query { get; set; }

        public List<BandaSearchResultViewModel> BandasResult { get; set; }
        public List<MusicaSearchResultViewModel> MusicasResult { get; set; }

        public List<PlaylistViewModel> UserPlaylists { get; set; }
        public int SelectedItemId { get; set; } 
        public string SelectedItemType { get; set; } 
        [Required]
        public int SelectedPlaylistId { get; set; } 

        public SearchViewModel()
        {
            Query = string.Empty;
            BandasResult = new List<BandaSearchResultViewModel>();
            MusicasResult = new List<MusicaSearchResultViewModel>();
            UserPlaylists = new List<PlaylistViewModel>();
        }
    }
}