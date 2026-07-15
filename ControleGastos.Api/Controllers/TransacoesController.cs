using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Rota: /api/transacoes
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] TransacaoCreateDTO dto)
    {
        try
        {
            var transacao = await _transacaoService.CadastrarAsync(dto);
            return CreatedAtAction(nameof(Cadastrar), new { id = transacao.Id }, transacao);
        }
        catch (ArgumentException ex)
        {
            // Se a regra de idade for violada, retorna HTTP 400 (Bad Request) com a mensagem
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            // Se a pessoa não existir, também retorna 400.
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var transacoes = await _transacaoService.ListarTodasAsync();
        return Ok(transacoes);
    }
}