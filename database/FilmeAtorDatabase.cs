using Microsoft.EntityFrameworkCore;

namespace apifilmes.Database
{
    public class FilmeAtorDatabase
    {
        Models.ApiDbContext ctx = new Models.ApiDbContext();

        public Models.TbFilmeAtor Salvar(Models.TbFilmeAtor fa)
        {
            ctx.TbFilmeAtors.Add(fa);
            ctx.SaveChanges();

            return fa;
        }


        public void SalvarEncadeado(List<Models.TbAtor> atores)
        {
            ctx.TbAtors.AddRange(atores);
            ctx.SaveChanges();
        }


        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes
                                    .Include(x => x.TbFilmeAtors)
                                    .ThenInclude(x => x.IdAtorNavigation)
                                    .ToList();

            return filmes;
        }


        public List<Models.TbDiretor> Consultar(string diretor, string genero)
        {
            List<Models.TbDiretor> diretores = ctx.TbDiretors
                    .Where(x => x.NmDiretor.Contains(diretor) && x.IdFilmeNavigation.DsGenero.Contains(genero))
                    .ToList();

            return diretores;
        }


        public List<Models.TbFilme> ConsultarFilmes(string genero, string diretor)
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes
                            .Include(x => x.TbDiretors)
                            .Where(x => x.DsGenero == genero
                                     && x.TbDiretors.All(d => d.NmDiretor.StartsWith(diretor)))
                            .ToList();

            return filmes;
        }
    }
}