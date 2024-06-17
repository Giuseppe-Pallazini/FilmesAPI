namespace apifilmes.Business
{
    public class FilmeBusiness
    {
        database.FilmeDatabase filmeDB = new database.FilmeDatabase();

        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            if (string.IsNullOrEmpty(filme.NmFilme))
                throw new ArgumentException("Nome é obrigatório!");

            if (string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Genero é obrigatório!");

            if (filme.VlAvaliacao < 0)
                throw new ArgumentException("Avaliação inválida!");

            if (filme.NrDuracao < 0)
                throw new ArgumentException("Duração Inválida!");


            if (filmeDB.FilmeExistente(filme.NmFilme))
                throw new ArgumentException("Filme já existe");


            Models.TbFilme f = filmeDB.Salvar(filme);
            return f;
        }
    }
}