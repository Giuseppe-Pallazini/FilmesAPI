using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using apifilmes.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        
        [HttpPost]
        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            ctx.TbFilmes.Add(filme);
            ctx.SaveChanges();

            return filme;
        }


        [HttpGet]
        public List<Models.TbFilme> Listar()
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();
            
            List<Models.TbFilme> filmes = ctx.TbFilmes.ToList();
            return filmes;
        }



        [HttpGet("consultar")]
        public List<Models.TbFilme> Consultar(string genero)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = ctx.TbFilmes.Where(x => x.DsGenero == genero)
                                                      .ToList();
            return filmes;
        }


        [HttpPut]
        public void Alterar(Models.TbFilme filme)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            Models.TbFilme atual = ctx.TbFilmes.First(x => x.IdFilme == filme.IdFilme);
            atual.NmFilme = filme.NmFilme;
            atual.DsGenero = filme.DsGenero;
            atual.NrDuracao = filme.NrDuracao;
            atual.VlAvaliacao = filme.VlAvaliacao;
            atual.BtDisponivel = filme.BtDisponivel;
            atual.DtLancamento = filme.DtLancamento;

            ctx.SaveChanges();
        }


        [HttpDelete]
        public void Remover(Models.TbFilme filme)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            Models.TbFilme atual = ctx.TbFilmes.First(x => x.IdFilme == filme.IdFilme);
            ctx.TbFilmes.Remove(atual);

            ctx.SaveChanges();
        }


        [HttpDelete("genero")]
        public void RemoverPorGenero(Models.TbFilme filme)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = ctx.TbFilmes.Where(x => x.DsGenero == filme.DsGenero)
                                                      .ToList();

            ctx.TbFilmes.RemoveRange(filmes);
            ctx.SaveChanges();
        }




        [HttpGet("testes/1")]
        public List<Models.TbFilme> ListarTestes1()
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = ctx.TbFilmes
                                             .Include(x => x.TbFilmeAtors)
                                             .ThenInclude(x => x.IdAtorNavigation)
                                             .ToList();


            return filmes;
        }



        [HttpGet("testes/2")]
        public List<Models.Responses.FilmeTestesResponse> ListarTestes2()
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

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




        [HttpPost("juntoemisturado")]
        public Models.Responses.TestesResponse InserirFilmeAtoresDiretor(Models.Request.FilmeAtorDiretorJuntoTestesRequest req)
        {
            Models.TbFilme filme = new Models.TbFilme();
            filme.NmFilme = req.Filme;
            filme.DsGenero = req.Genero;
            filme.NrDuracao = req.Duracao;
            filme.VlAvaliacao = req.Avaliacao;
            filme.BtDisponivel = req.Disponivel;
            filme.DtLancamento = req.Lancamento;

            filme.TbDiretors = new List<TbDiretor>
            {
                new Models.TbDiretor
                {
                    NmDiretor = req.Diretor.Nome,
                    DtNascimento = req.Diretor.Nascimento
                }
            };

            filme.TbFilmeAtors =
                req.Atores.Select(x => new Models.TbFilmeAtor()
                {
                    NmPersonagem = x.Personagem,
                    IdAtorNavigation = new Models.TbAtor()
                    {
                        NmAtor = x.Ator,
                        VlAltura = x.Altura,
                        DtNascimento = x.Nascimento
                    }
                }).ToList();


        Models.ApiDbContext ctx = new Models.ApiDbContext();

        ctx.TbFilmes.Add(filme);
        ctx.SaveChanges();

        Models.Responses.TestesResponse resp = new Models.Responses.TestesResponse();
        resp.Resposta = "Filme Adicionado com sucesso!";
        return resp;
        }


    }
}