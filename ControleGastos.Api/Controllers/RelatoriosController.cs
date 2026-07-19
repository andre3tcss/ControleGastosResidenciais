using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    /// <summary>
    /// Consolida os dados financeiros do sistema, gerando o balanço individualizado e os acumulados globais.
    /// </summary>
    /// <returns>Payload estruturado com receitas, despesas e saldo líquido de cada indivíduo e do balanço geral.</returns>
    [HttpGet("totais")]
    public async Task<IActionResult> ObterTotais()
    {
        var relatorio = await _relatorioService.ObterTotaisGeraisAsync();
        return Ok(relatorio);
    }
}