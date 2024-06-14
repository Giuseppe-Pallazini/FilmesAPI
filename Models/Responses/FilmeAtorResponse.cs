namespace apifilmes.Models.Responses
{
    public class FilmeAtorResponse
    {
        public FilmeAtorItemFilmeResponse? Filme { get; set; }
        public List<FilmeAtorItemAtorResponse>? Atores { get; set; }
    }
}