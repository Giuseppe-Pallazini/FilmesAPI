using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using apifilmes.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Utils
{


    // Responsável por converter todas informações do modelo do BD para o modelo do Response


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



        public Models.TbFilme ConverterFilmeAtoresDiretor(Models.Request.FilmeAtorDiretorJuntoTestesRequest req)
        {
            Models.TbFilme filme = new Models.TbFilme();
            filme.NmFilme = req.Filme;
            filme.DsGenero = req.Genero;
            filme.NrDuracao = req.Duracao;
            filme.VlAvaliacao = req.Avaliacao;
            filme.BtDisponivel = req.Disponivel;
            filme.DtLancamento = req.Lancamento;

            filme.TbDiretors = new List<TbDiretor>
            {
                new Models.TbDiretor
                {
                    NmDiretor = req.Diretor.Nome,
                    DtNascimento = req.Diretor.Nascimento
                }
            };

            filme.TbFilmeAtors =
                req.Atores.Select(x => new Models.TbFilmeAtor()
                {
                    NmPersonagem = x.Personagem,
                    IdAtorNavigation = new Models.TbAtor()
                    {
                        NmAtor = x.Ator,
                        VlAltura = x.Altura,
                        DtNascimento = x.Nascimento
                    }
                }).ToList();

            return filme;
        }
    }
}