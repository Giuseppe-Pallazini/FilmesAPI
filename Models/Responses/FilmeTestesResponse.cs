namespace apifilmes.Models.Responses
{
    public class FilmeTestesResponse
    {
        public string? Filme { get; set; }
        public string? Genero { get; set; }
        public int? Duracao { get; set; }
        public bool? Disponivel { get; set; }
        public List<FilmeTestesAtorResponse> Personagens { get; set; }
    }
}