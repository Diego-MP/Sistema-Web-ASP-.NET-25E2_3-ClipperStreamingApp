using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClipperStreamingApp.WebApp.Models
{
    
    public class MusicaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeBanda { get; set; }
    }

    public class MusicasWrapper
    {
        [JsonPropertyName("$values")]
        public List<MusicaViewModel> Values { get; set; }
    }
    
    public class PlaylistViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeMusicas { get; set; }
    }
    
    public class PlaylistDetalhesViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public MusicasWrapper Musicas { get; set; }
    }
    
    public class ApiResponseWrapper<T>
    {
        [JsonPropertyName("$values")]
        public List<T> Values { get; set; }
    }
}