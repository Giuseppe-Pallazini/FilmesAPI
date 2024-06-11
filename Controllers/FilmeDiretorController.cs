using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FilmeDiretorController : ControllerBase
    {




        [HttpPost]
        public Models.Request.FilmeDiretorRequest Salvar(Models.Request.FilmeDiretorRequest request)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            ctx.TbFilmes.Add(request.Filme);
            ctx.SaveChanges();

            request.Diretor.IdFilme = request.Filme.IdFilme;

            ctx.TbDiretors.Add(request.Diretor);
            ctx.SaveChanges();


            return request;
        }


        [HttpPost("encadeado")]
        public Models.TbFilme SalvarEncadeado(Models.TbFilme filme)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            ctx.TbFilmes.Add(filme);
            ctx.SaveChanges();

            return filme;
        }





        [HttpGet("consultar/diretor")]
        public List<Models.TbDiretor> Consultar(string diretor, string genero)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbDiretor> diretores = ctx.TbDiretors
                    //.Include(x => x.IdFilmeNavigation)
                    .Where(x => x.NmDiretor.Contains(diretor) && x.IdFilmeNavigation.DsGenero.Contains(genero))
                    .ToList();

            return diretores;
        }






        [HttpGet("consultar/filmes")]
        public List<Models.TbFilme> ConsultarFilmes(string genero, string diretor)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = ctx.TbFilmes
                            .Include(x => x.TbDiretors)
                            .Where(x => x.DsGenero == genero
                                     && x.TbDiretors.All(d => d.NmDiretor.StartsWith(diretor)))
                            .ToList();

            return filmes;
        }
    }
}