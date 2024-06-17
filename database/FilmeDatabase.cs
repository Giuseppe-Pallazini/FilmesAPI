using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace apifilmes.database
{
    public class FilmeDatabase
    {
        Models.ApiDbContext ctx = new Models.ApiDbContext();

        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            ctx.TbFilmes.Add(filme);
            ctx.SaveChanges();

            return filme;
        }


        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes.ToList();
            return filmes;
        }


        public List<Models.TbFilme> ConsultarPorGenero(string genero)
        {

            List<Models.TbFilme> filmes = ctx.TbFilmes.Where(x => x.DsGenero == genero)
                                                      .ToList();
            return filmes;
        }


        public void Alterar(Models.TbFilme filme)
        {
            Models.TbFilme atual = ctx.TbFilmes.First(x => x.IdFilme == filme.IdFilme);
            atual.NmFilme = filme.NmFilme;
            atual.DsGenero = filme.DsGenero;
            atual.NrDuracao = filme.NrDuracao;
            atual.VlAvaliacao = filme.VlAvaliacao;
            atual.BtDisponivel = filme.BtDisponivel;
            atual.DtLancamento = filme.DtLancamento;

            ctx.SaveChanges();
        }


        public void Remover(Models.TbFilme filme)
        {
            Models.TbFilme atual = ctx.TbFilmes.First(x => x.IdFilme == filme.IdFilme);
            ctx.TbFilmes.Remove(atual);

            ctx.SaveChanges();
        }


        public void RemoverPorGenero(Models.TbFilme filme)
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes.Where(x => x.DsGenero == filme.DsGenero)
                                                      .ToList();

            ctx.TbFilmes.RemoveRange(filmes);
            ctx.SaveChanges();
        }


        public List<Models.TbFilme> ListarTestes1()
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes
                                             .Include(x => x.TbFilmeAtors)
                                             .ThenInclude(x => x.IdAtorNavigation)
                                             .ToList();


            return filmes;
        }


        public List<Models.Responses.FilmeTestesResponse> ListarTestes2()
        {
            List<Models.TbFilme> filmes = ctx.TbFilmes
                                             .Include(x => x.TbFilmeAtors)
                                             .ThenInclude(x => x.IdAtorNavigation)
                                             .ToList();

            List<Models.Responses.FilmeTestesResponse> response =
                filmes.Select(x => new Models.Responses.FilmeTestesResponse()
                {
                    Filme = new Models.Responses.FilmeAtorItemFilmeResponse()
                    {
                    Id = x.IdFilme,
                    Filme = x.NmFilme,
                    Genero = x.DsGenero,
                    Duracao = x.NrDuracao,
                    Avaliacao = x.VlAvaliacao,
                    Disponivel = x.BtDisponivel,
                    Lancamento = x.DtLancamento
                },
                    Personagens = x.TbFilmeAtors
                                   .Select(y => new Models.Responses.FilmeTestesAtorResponse()
                                {
                                    Ator = y.IdAtorNavigation.NmAtor,
                                    Personagem = y.NmPersonagem,
                                    Nascimento = y.IdAtorNavigation.DtNascimento
                                }).ToList()
                }).ToList();

            return response;
        }


        public List<Models.TbFilme> Consultar(string genero, string personagem, string ator)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = 
                ctx.TbFilmes
                        .Include(x => x.TbFilmeAtors)
                        .ThenInclude(x => x.IdAtorNavigation)
                    .Where(y => y.DsGenero == genero 
                            && y.TbFilmeAtors.Any(a => a.NmPersonagem.Contains(personagem) 
                                                    && a.IdAtorNavigation.NmAtor.Contains(ator) ))
                    .ToList();

            return filmes;
        }


        public bool FilmeExistentePorNome(string nome)
        {
            bool filmes =  ctx.TbFilmes.Any(y => y.NmFilme == nome);
            return filmes;
        }

        public bool FilmeExistentePorId(int id)
        {
            bool filmes =  ctx.TbFilmes.Any(y => y.IdFilme == id);
            return filmes;
        }

        public bool DiretorExistentePorNome(string nome)
        {
            bool diretor = ctx.TbDiretors.Any(y => y.NmDiretor == nome);
            return diretor;
        }

        public Models.TbFilme ConsultarPorNomeFilme(string nome)
        {
            Models.TbFilme filme = ctx.TbFilmes.First(x => x.NmFilme == nome);
            return filme;
        }
    }
}