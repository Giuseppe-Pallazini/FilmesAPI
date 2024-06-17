using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace apifilmes.database
{
    public class FilmeDatabase
    {
        Models.ApiDbContext ctx = new Models.ApiDbContext();

        
        public List<Models.TbFilme> Consultar(string genero, string personagem, string ator)
        {
            Models.ApiDbContext ctx = new Models.ApiDbContext();

            List<Models.TbFilme> filmes = 
                ctx.TbFilmes
                        .Include(x => x.TbFilmeAtors)
                        .ThenInclude(x => x.IdAtorNavigation)
                    .Where(y => y.DsGenero == genero 
                            && y.TbFilmeAtors.Any(a => a.NmPersonagem.Contains(personagem) 
                                                    && a.IdAtorNavigation.NmAtor.Contains(ator) ))
                    .ToList();

            return filmes;
        }


        public bool FilmeExistente(string nome)
        {
            bool filmes =  ctx.TbFilmes.Any(y => y.NmFilme == nome);
            return filmes;
        }


        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            ctx.TbFilmes.Add(filme);
            ctx.SaveChanges();

            return filme;
        }
    }
}