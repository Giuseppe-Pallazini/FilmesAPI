namespace apifilmes.Models.Request
{
    public class AtorTesteRequest
    {
        public string? Ator { get; set; }
        public string? Personagem { get; set; }
        public decimal Altura { get; set; }
        public DateOnly Nascimento { get; set; }
    }
}