using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Connections;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeAtorController : ControllerBase
    {

        Database.AtorDatabase atorDB = new Database.AtorDatabase();
        Database.FilmeAtorDatabase filmeAtorDB = new Database.FilmeAtorDatabase();
        database.FilmeDatabase filmeDB = new database.FilmeDatabase();


        [HttpPost]
        public void Salvar(Models.Request.FilmeAtorRequest request)
        {


            foreach (Models.Request.FilmeAtorItemRequest item in request.Atores)
            {
                Models.TbAtor ator = new Models.TbAtor();
                ator.NmAtor = item.Ator;
                ator.VlAltura = item.Altura;
                ator.DtNascimento = item.Nascimento;

                atorDB.Salvar(ator);


                Models.TbFilmeAtor fa = new Models.TbFilmeAtor();
                fa.NmPersonagem = item.Personagem;
                fa.IdFilme = request.IdFilme;
                fa.IdAtor = ator.IdAtor;

                filmeAtorDB.Salvar(fa);
            }

        }


        [HttpPost("encadeado")]
        public void SalvarEncadeado(List<Models.TbAtor> atores)
        {
            filmeAtorDB.SalvarEncadeado(atores);
        }


        [HttpPost("mix")]
        public void SalvarMix(Models.Request.FilmeAtorRequest request)
        {

            List<Models.TbAtor> atores =
                request.Atores.Select(x => new Models.TbAtor()
                {
                    NmAtor = x.Ator,
                    VlAltura = x.Altura,
                    DtNascimento = x.Nascimento,
                    TbFilmeAtors = new List<Models.TbFilmeAtor>() {
                        new Models.TbFilmeAtor()
                        {
                            NmPersonagem = x.Personagem,
                            IdFilme = request.IdFilme
                        }
                    }
                }).ToList();

            filmeAtorDB.SalvarEncadeado(atores);
        }


        [HttpPost("juntos")]
        public void SalvarJunto(Models.Request.FilmeAtorJuntosRequest req)
        {

            Models.TbFilme filme = new Models.TbFilme();
            filme.NmFilme = req.NmFIlme;
            filme.DsGenero = req.DsGenero;
            filme.NrDuracao = req.NrDuracao;
            filme.VlAvaliacao = req.VlAvaliacao;
            filme.BtDisponivel = req.BtDisponivel;
            filme.DtLancamento = req.DtLancamento;

            filme.TbFilmeAtors =
                req.Atores.Select(x => new Models.TbFilmeAtor()
                {
                    NmPersonagem = x.Personagem,
                    IdAtorNavigation = new Models.TbAtor()
                    {
                        NmAtor = x.Ator,
                        DtNascimento = x.Nascimento,
                        VlAltura = x.Altura
                    }
                }).ToList();


            filmeDB.Salvar(filme);
        }


        [HttpGet]
        public List<Models.Responses.FilmeAtorResponse> Listar()
        {

            List<Models.TbFilme> filmes = filmeAtorDB.Listar();

            List<Models.Responses.FilmeAtorResponse> response = 
                filmes.Select(x => new Models.Responses.FilmeAtorResponse()
                {
                    Filme = new Models.Responses.FilmeAtorItemFilmeResponse()
                    {
                        Id = x.IdFilme,
                        Filme = x.NmFilme,
                        Genero = x.DsGenero,
                        Avaliacao = x.VlAvaliacao,
                        Duracao = x.NrDuracao,
                        Disponivel = x.BtDisponivel,
                        Lancamento = x.DtLancamento,

                    },
                    Atores = x.TbFilmeAtors.Select(f => new Models.Responses.FilmeAtorItemAtorResponse()
                    {
                        IdAtor = f.IdAtor,
                        IdFilmeAtor = f.IdFilmeAtor,
                        Ator = f.IdAtorNavigation.NmAtor,
                        Personagem = f.NmPersonagem
                    }).ToList()

                }).ToList();
        
            return response;
        }


    }
}