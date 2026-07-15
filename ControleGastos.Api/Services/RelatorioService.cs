using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class RelatorioService : IRelatorioService
{
    private readonly AppDbContext _context;

    public RelatorioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RelatorioGeralDTO> ObterTotaisGeraisAsync()
    {
        // 1. Busca todas as pessoas e faz um "JOIN" (Include) com as transações delas
        var pessoasComTransacoes = await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();

        var relatorio = new RelatorioGeralDTO();

        // 2. Transforma cada Entidade em um DTO de Total usando LINQ (.Select)
        relatorio.Pessoas = pessoasComTransacoes.Select(p => new PessoaTotalDTO
        {
            Nome = p.Nome,

            // LINQ: Soma (.Sum) apenas onde o tipo for Receita
            TotalReceitas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor),

            // LINQ: Soma (.Sum) apenas onde o tipo for Despesa
            TotalDespesas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor)
        }).ToList();

        // 3. Calcula os Totais Globais somando os totais individuais de todos
        relatorio.TotalGeralReceitas = relatorio.Pessoas.Sum(p => p.TotalReceitas);
        relatorio.TotalGeralDespesas = relatorio.Pessoas.Sum(p => p.TotalDespesas);

        return relatorio;
    }
}