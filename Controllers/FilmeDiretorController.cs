using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

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

        
    }
}