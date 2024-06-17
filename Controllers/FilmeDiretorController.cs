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

        database.FilmeDatabase filmeDB = new database.FilmeDatabase();
        Database.DiretorDatabase diretorDB = new Database.DiretorDatabase();
        Database.FilmeAtorDatabase faDB = new Database.FilmeAtorDatabase();


        [HttpPost]
        public Models.Request.FilmeDiretorRequest Salvar(Models.Request.FilmeDiretorRequest request)
        {
            filmeDB.Salvar(request.Filme);

            request.Diretor.IdFilme = request.Filme.IdFilme;

            diretorDB.Salvar(request.Diretor);

            return request;
        }


        [HttpPost("encadeado")]
        public Models.TbFilme SalvarEncadeado(Models.TbFilme filme)
        {
            filmeDB.Salvar(filme);

            return filme;
        }


        [HttpGet("consultar/diretor")]
        public List<Models.TbDiretor> Consultar(string diretor, string genero)
        {
            List<Models.TbDiretor> diretors = faDB.Consultar(diretor, genero);
            return diretors;
        }


        [HttpGet("consultar/filmes")]
        public List<Models.TbFilme> ConsultarFilmes(string genero, string diretor)
        {
            List<Models.TbFilme> filmes = faDB.ConsultarFilmes(genero, diretor);
            return filmes;
        }
    }
}