using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        database.FilmeDatabase filmeDB = new database.FilmeDatabase();

        Database.DiretorDatabase diretorDB = new Database.DiretorDatabase();

        [HttpPost]
        public Models.TbDiretor Salvar(Models.TbDiretor diretor)
        {
            Models.TbDiretor resp = diretorDB.Salvar(diretor);
            return resp;
        }


        
        [HttpPost("filme")]
        public Models.responses.DiretorPorFilmeNomeResponse SalvarPorFilmeNome(Models.Request.DiretorPorFilmeNome diretorReq)
        {
            Models.TbFilme filme = filmeDB.ConsultarPorNomeFilme(diretorReq.NmFilme);

            Models.TbDiretor diretor = new Models.TbDiretor();
            diretor.NmDiretor = diretorReq.NmDiretor;
            diretor.DtNascimento = diretorReq.DtNascimento;
            diretor.IdFilme = filme.IdFilme;


            diretorDB.Salvar(diretor);


            Models.responses.DiretorPorFilmeNomeResponse resp = new Models.responses.DiretorPorFilmeNomeResponse();
            resp.IdDiretor = diretor.IdDiretor;
            resp.NmDiretor = diretor.NmDiretor;
            resp.IdFilme = filme.IdFilme;
            resp.NmFilme = filme.NmFilme;
            resp.DtNascimento = diretor.DtNascimento;

            return resp;
            }



        [HttpGet]
        public List<Models.responses.DiretorResponse> Listar()
        {
            List<Models.TbDiretor> diretors = diretorDB.Listar();

            List<Models.responses.DiretorResponse> response =
                diretors.Select(x => new Models.responses.DiretorResponse {
                    IdDiretor = x.IdDiretor,
                    IdFilme = x.IdFilme,
                    Filme = x.IdFilmeNavigation.NmFilme,
                    Diretor = x.NmDiretor,
                    Genero = x.IdFilmeNavigation.DsGenero,
                    Disponivel = x.IdFilmeNavigation.BtDisponivel
                }).ToList();

            return response;
        }


        [HttpPut]
        public void Alterar(Models.TbDiretor diretor)
        {
            diretorDB.Alterar(diretor);
        }


        [HttpDelete]
        public void Deletar(Models.TbDiretor diretor)
        {
            diretorDB.Deletar(diretor);
        }
    }
}