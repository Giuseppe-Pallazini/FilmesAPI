using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;


namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class atorController : ControllerBase
    {
        
        public Models.TbAtor Salvar(Models.TbAtor ator)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();
            
            ctx.TbAtors.Add(ator);
            ctx.SaveChanges();

            return ator;
        }
    }
}