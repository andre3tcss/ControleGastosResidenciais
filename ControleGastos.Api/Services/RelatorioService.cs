using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

/// <summary>
/// Provedor de serviços especializado na consolidação e cálculo de indicadores de auditoria financeira residencial.
/// </summary>
public class RelatorioService : IRelatorioService
{
    private readonly AppDbContext _context;

    public RelatorioService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Consolida de forma assíncrona o balanço financeiro global, agrupando os saldos individuais e calculando os acumulados líquidos.
    /// </summary>
    /// <returns>Uma tarefa que resulta no modelo de transferência de dados (DTO) do relatório consolidado.</returns>
    public async Task<RelatorioGeralDTO> ObterTotaisGeraisAsync()
    {
        // Garante o carregamento antecipado (Eager Loading) da coleção de transações vinculadas a cada morador para evitar problemas de N+1 consultas na base de dados
        var pessoasComTransacoes = await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();

        var relatorio = new RelatorioGeralDTO();

        // Mapeia a projeção das entidades de domínio para a estrutura de apresentação individualizada de balanços
        relatorio.Pessoas = pessoasComTransacoes.Select(p => new PessoaTotalDTO
        {
            Nome = p.Nome,

            // Regra de negócio: Consolida o montante acumulado correspondente aos fluxos de entrada financeira do morador
            TotalReceitas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor),

            // Regra de negócio: Consolida o montante acumulado correspondente aos fluxos de saída e despesas do morador
            TotalDespesas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor)
        }).ToList();

        // Agrega os indicadores acumulados em nível global para fechamento e auditoria do balanço do ecossistema residencial
        relatorio.TotalGeralReceitas = relatorio.Pessoas.Sum(p => p.TotalReceitas);
        relatorio.TotalGeralDespesas = relatorio.Pessoas.Sum(p => p.TotalDespesas);

        return relatorio;
    }
}
