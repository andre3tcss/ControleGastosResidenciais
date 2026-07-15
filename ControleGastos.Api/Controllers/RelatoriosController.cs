using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Rota: /api/relatorios
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("totais")] // Rota final: /api/relatorios/totais
    public async Task<IActionResult> ObterTotais()
    {
        var relatorio = await _relatorioService.ObterTotaisGeraisAsync();
        return Ok(relatorio);
    }
}