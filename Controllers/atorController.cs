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
        Database.AtorDatabase atorDB = new Database.AtorDatabase(); 
        public Models.TbAtor Salvar(Models.TbAtor ator)
        {
            Models.TbAtor resp = atorDB.Salvar(ator);
            return resp;
        }
    }
}