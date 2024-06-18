using apifilmes.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace apifilmes.Business
{

    // Responsável por toda a regra de negócio e validações do sistema

    public class FilmeBusiness
    {
        database.FilmeDatabase filmeDB = new database.FilmeDatabase();

        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            ValidacoesFilme(filme);

            Models.TbFilme f = filmeDB.Salvar(filme);
            return f;
        }


        public void ValidacoesFilme(TbFilme filme)
        {
            if (string.IsNullOrEmpty(filme.NmFilme))
                throw new ArgumentException("Nome do filme é obrigatório!");

            if (string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Genero do filme é obrigatório!");

            if (filme.VlAvaliacao < 0)
                throw new ArgumentException("Avaliação do filme inválida!");

            if (filme.NrDuracao <=   0)
                throw new ArgumentException("Duração do filme inválida!");

            if(filme.DtLancamento == null)
                throw new ArgumentException("Data de lançamento do filme inválida!");


            if (filmeDB.FilmeExistentePorNome(filme.NmFilme))
                throw new ArgumentException("Filme já existe");

        }


        public void ValidacoesDiretor(TbDiretor diretor)
        {
            if (string.IsNullOrEmpty(diretor.NmDiretor))
                throw new ArgumentException("Nome do diretor é obrigatório!");

            if (diretor.DtNascimento == null)
                throw new ArgumentException("Data de nascimento do diretor inválida!");



            if (filmeDB.DiretorExistentePorNome(diretor.NmDiretor))
                throw new ArgumentException("Diretor já existe");
        }


        public void ValidacoesAtores(List<Models.Request.AtorTesteRequest> atores)
        {
            int index = 0;
             foreach (Models.Request.AtorTesteRequest item in atores)
             {
                index++;
                Models.TbAtor ator = new Models.TbAtor();


                if(string.IsNullOrEmpty(item.Ator))
                    throw new ArgumentException("Nome do " + index + "° ator obrigatório!");

                if(item.Altura <= 0)
                    throw new ArgumentException("Altura do " + index + "° ator inválida");

                if(item.Nascimento == null)
                    throw new ArgumentException("Data de nascimento  do " + index + "° ator inválida");


                Models.TbFilmeAtor fa = new TbFilmeAtor();
                if(string.IsNullOrEmpty(item.Personagem))
                    throw new ArgumentException("Nome de personagem do " + index + "° ator obrigatório!");
             }
        }

        public List<Models.TbFilme> ConsultarPorGenero(string genero)
        {
            if(string.IsNullOrEmpty(genero))
                throw new ArgumentException("Genero Inválido");

            List<Models.TbFilme> filmes = filmeDB.ConsultarPorGenero(genero);
            return filmes;
        }
    
    
        public void Alterar(Models.TbFilme filme)
        {
            ValidacoesFilme(filme);

            filmeDB.Alterar(filme);
        }
    
    
        public void Remover(Models.TbFilme filme)
        {
            if(filme.IdFilme.GetType() != typeof(int))
                throw new ArgumentException("ID inválido!");

            if(!filmeDB.FilmeExistentePorId(filme.IdFilme))
                throw new ArgumentException("Filme não encontrado");

            filmeDB.Remover(filme);
        }
    
    
        public void RemoverPorGenero(Models.TbFilme filme)
        {
            if(string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Genero inválido!");

            filmeDB.RemoverPorGenero(filme);
        }

    
        public Models.TbFilme InserirFilmeAtoresDiretor(Models.Request.FilmeAtorDiretorJuntoTestesRequest req)
        {
            Models.TbFilme filme = new TbFilme();
            filme.NmFilme = req.Filme;
            filme.DsGenero = req.Genero;
            filme.NrDuracao = req.Duracao;
            filme.VlAvaliacao = req.Avaliacao;
            filme.BtDisponivel = req.Disponivel;
            filme.DtLancamento = req.Lancamento;

            ValidacoesFilme(filme);


            Models.TbDiretor diretor = new TbDiretor();
            diretor.NmDiretor = req.Diretor.Nome;
            diretor.DtNascimento = req.Diretor.Nascimento;

            ValidacoesDiretor(diretor);


            ValidacoesAtores(req.Atores);


            Utils.FilmeConverterResponse filmeConverter = new Utils.FilmeConverterResponse();
            Models.TbFilme filmeResp = filmeConverter.ConverterFilmeAtoresDiretor(req);

            return filmeDB.Salvar(filmeResp);
        }


        public List<Models.Responses.FilmeTestesResponse> Consultar(string genero, string personagem, string ator)
        {
            List<Models.TbFilme> filmes = filmeDB.Consultar(genero, personagem, ator); 

            Utils.FilmeConverterResponse faConverter = new Utils.FilmeConverterResponse();
            List<Models.Responses.FilmeTestesResponse> response = faConverter.Converter(filmes);

            return response;
        }
    }
}