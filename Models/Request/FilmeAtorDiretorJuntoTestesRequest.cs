namespace apifilmes.Models.Request
{
    public class FilmeAtorDiretorJuntoTestesRequest
    {
        public string? Filme { get; set; }
        public string? Genero { get; set; }
        public int Duracao { get; set; }
        public decimal Avaliacao { get; set; }
        public bool Disponivel { get; set; }
        public DateOnly Lancamento { get; set; }

        //Diretor
        public DiretorTesteRequest? Diretor { get; set; }

        //Atores
        public List<AtorTesteRequest>? Atores { get; set; }
    }
}