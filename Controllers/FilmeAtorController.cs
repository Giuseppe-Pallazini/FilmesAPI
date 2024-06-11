using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Connections;

using Microsoft.AspNetCore.Mvc;

namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeAtorController : ControllerBase
    {
        
        [HttpPost]
        public void Salvar(Models.Request.FilmeAtorRequest request)
        {

            Models.ApiDbContext ctx = new Models.ApiDbContext();

            foreach (Models.Request.FilmeAtorItemRequest item in request.Atores)
            {
                Models.TbAtor ator = new Models.TbAtor();
                ator.NmAtor = item.Ator;
                ator.VlAltura = item.Altura;
                ator.DtNascimento = item.Nascimento;

                ctx.TbAtors.Add(ator);
                ctx.SaveChanges();


                Models.TbFilmeAtor fa = new Models.TbFilmeAtor();
                fa.NmPersonagem = item.Personagem;
                fa.IdFilme = request.IdFilme;
                fa.IdAtor = ator.IdAtor;
                
                ctx.TbFilmeAtors.Add(fa);
                ctx.SaveChanges();
            }

        }



        [HttpPost("encadeado")]
        public void SalvarEncadeado(List<Models.TbAtor> atores)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();
            ctx.TbAtors.AddRange(atores);
            ctx.SaveChanges();
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

            Models.ApiDbContext ctx = new Models.ApiDbContext();
            ctx.TbAtors.AddRange(atores);
            ctx.SaveChanges();

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

            Models.ApiDbContext ctx = new Models.ApiDbContext();
            ctx.TbFilmes.Add(filme);
            ctx.SaveChanges();

        }



        }
    }