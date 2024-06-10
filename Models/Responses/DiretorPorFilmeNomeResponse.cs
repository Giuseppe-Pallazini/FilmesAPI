

namespace apifilmes.Models.responses
{
    public class DiretorPorFilmeNomeResponse
        {
        public int IdDiretor { get; set; }
        public int? IdFilme { get; set; }
        public string? NmDiretor { get; set; }
        public DateOnly? DtNascimento { get; set; }
        public string? NmFilme { get; set; }
    }
}