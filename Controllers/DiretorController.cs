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
        public List<Models.TbDiretor> Listar()
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbDiretor> diretors = 
                ctx.TbDiretors.Include(x => x.IdFilmeNavigation).ToList();

            return diretors;
        }
    }
}