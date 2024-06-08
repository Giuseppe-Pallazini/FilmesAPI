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
    }
}