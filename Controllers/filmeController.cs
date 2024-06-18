using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using apifilmes.Models;
using apifilmes.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        database.FilmeDatabase filmeDB = new database.FilmeDatabase();
        Business.FilmeBusiness filmeBusiness = new Business.FilmeBusiness();


        [HttpPost]
        public ActionResult<Models.TbFilme> Salvar(Models.TbFilme filme)
        {
            try
            {
                Models.TbFilme f = filmeBusiness.Salvar(filme);
                return f;
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(
                    new Models.Responses.ErrorResponse(ex, 400)
                );
            }
        }


        [HttpGet]
        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = filmeDB.Listar();
            return filmes;
        }


        [HttpGet("consultar")]
        public List<Models.TbFilme> ConsultarPorGenero(string genero)
        {
            List<Models.TbFilme> filmes = filmeBusiness.ConsultarPorGenero(genero);
            return filmes;
        }


        [HttpPut]
        public void Alterar(Models.TbFilme filme)
        {
            filmeBusiness.Alterar(filme);
        }


        [HttpDelete]
        public void Remover(Models.TbFilme filme)
        {
            filmeBusiness.Remover(filme);
        }


        [HttpDelete("genero")]
        public void RemoverPorGenero(Models.TbFilme filme)
        {
            filmeBusiness.RemoverPorGenero(filme);
        }


        [HttpGet("testes/1")]
        public List<Models.TbFilme> ListarTestes1()
        {
            List<Models.TbFilme> filmes = filmeDB.ListarTestes1();
            return filmes;
        }


        [HttpGet("testes/2")]
        public List<Models.Responses.FilmeTestesResponse> ListarTestes2()
        {
            List<Models.Responses.FilmeTestesResponse> filmes = filmeDB.ListarTestes2();
            return filmes;
        }


        [HttpGet("testes/3")]
        public List<Models.Responses.FilmeTestesResponse> ListarTestes3(string genero, string personagem, string ator)
        {
            List<Models.Responses.FilmeTestesResponse> filmes = filmeBusiness.Consultar(genero, personagem, ator);
            return filmes;
        }


        [HttpPost("juntoemisturado")]
        public ActionResult<Models.TbFilme> InserirFilmeAtoresDiretor(Models.Request.FilmeAtorDiretorJuntoTestesRequest req)
        {
            try
            {
                return filmeBusiness.InserirFilmeAtoresDiretor(req);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(
                    new Models.Responses.ErrorResponse(ex, 400)
                );
            }
        }


    }
}