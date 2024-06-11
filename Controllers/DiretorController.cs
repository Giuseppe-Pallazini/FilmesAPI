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

        
        [HttpPost]
        public Models.TbDiretor Salvar(Models.TbDiretor diretor)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            ctx.TbDiretors.Add(diretor);
            ctx.SaveChanges();

            return diretor;
        }


        
        [HttpPost("filme")]
        public Models.responses.DiretorPorFilmeNomeResponse SalvarPorFilmeNome(Models.Request.DiretorPorFilmeNome diretorReq)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            Models.TbFilme filme = ctx.TbFilmes.First(x => x.NmFilme == diretorReq.NmFilme);


            Models.TbDiretor diretor = new Models.TbDiretor();
            diretor.NmDiretor = diretorReq.NmDiretor;
            diretor.DtNascimento = diretorReq.DtNascimento;
            diretor.IdFilme = filme.IdFilme;


            ctx.TbDiretors.Add(diretor);
            ctx.SaveChanges();

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
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbDiretor> diretors = 
                ctx.TbDiretors.Include(x => x.IdFilmeNavigation).ToList();

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
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            Models.TbDiretor atual = ctx.TbDiretors.First(x => x.IdDiretor == diretor.IdDiretor);
            atual.NmDiretor = diretor.NmDiretor;
            atual.DtNascimento = diretor.DtNascimento;
            atual.IdFilme = diretor.IdFilme;

            ctx.SaveChanges();

        }



        [HttpDelete]
        public void Deletar(Models.TbDiretor diretor)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            Models.TbDiretor atual = ctx.TbDiretors.First(x => x.IdDiretor == diretor.IdDiretor);

            ctx.Remove(atual);
            ctx.SaveChanges();
        }
    }
}