using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using apifilmes.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Utils
{
    public class FilmeConverterResponse
    {
        

        public List<Models.Responses.FilmeTestesResponse> Converter(List<Models.TbFilme> filmes)
        {
            List<Models.Responses.FilmeTestesResponse> response =
                filmes.Select(x => new Models.Responses.FilmeTestesResponse()
                {
                    Filme = new Models.Responses.FilmeAtorItemFilmeResponse()
                    {
                    Id = x.IdFilme,
                    Filme = x.NmFilme,
                    Genero = x.DsGenero,
                    Duracao = x.NrDuracao,
                    Avaliacao = x.VlAvaliacao,
                    Disponivel = x.BtDisponivel,
                    Lancamento = x.DtLancamento
                },
                    Personagens = x.TbFilmeAtors
                                   .Select(y => new Models.Responses.FilmeTestesAtorResponse()
                                {
                                    Ator = y.IdAtorNavigation.NmAtor,
                                    Personagem = y.NmPersonagem,
                                    Nascimento = y.IdAtorNavigation.DtNascimento
                                }).ToList()
                }).ToList();

                return response;
        }
    }
}