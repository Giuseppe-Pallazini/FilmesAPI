using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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
    }
}