namespace apifilmes.Models.Responses
{
    public class FilmeTestesResponse
    {
        public FilmeAtorItemFilmeResponse? Filme { get; set; }
        public List<FilmeTestesAtorResponse>? Personagens { get; set; }
    }
}