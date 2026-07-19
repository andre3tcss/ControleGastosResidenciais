using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

/// <summary>
/// Contrato de serviço responsável pela agregação, auditoria e consolidação dos indicadores financeiros gerais do sistema.
/// </summary>
public interface IRelatorioService
{
    /// <summary>
    /// Consolida de forma assíncrona o balanço financeiro global, agrupando os saldos individuais e calculando os acumulados líquidos.
    /// </summary>
    /// <returns>Uma tarefa que resulta no modelo de transferência de dados (DTO) do relatório consolidado.</returns>
    Task<RelatorioGeralDTO> ObterTotaisGeraisAsync();
}
