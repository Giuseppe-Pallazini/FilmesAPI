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
    }
}