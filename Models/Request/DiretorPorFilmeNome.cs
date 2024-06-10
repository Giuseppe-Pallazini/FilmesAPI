using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace apifilmes.Models.Request
{
    public class DiretorPorFilmeNome
    {
        public string NmDiretor { get; set; }
        public DateOnly DtNascimento { get; set; }
        public string NmFilme { get; set; }
        
    }
}