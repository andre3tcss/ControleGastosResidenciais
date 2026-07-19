using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// Registra um novo lançamento financeiro, validando as restrições de domínio associadas ao perfil do morador.
    /// </summary>
    /// <param name="dto">Payload com as informações da transação (descrição, valor, tipo e vínculo com a pessoa).</param>
    /// <returns>A transação persistida ou uma falha de validação de domínio.</returns>
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
            // Intercepta violações das regras de negócio (ex: restrição de idade) e retorna status HTTP 400
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            // Trata falhas de integridade referencial quando a entidade associada não é localizada na base
            return BadRequest(new { erro = ex.Message });
        }
    }

    /// <summary>
    /// Recupera o histórico completo de transações financeiras registradas na base de dados.
    /// </summary>
    /// <returns>Coleção de DTOs representando receitas e despesas.</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var transacoes = await _transacaoService.ListarTodasAsync();
        return Ok(transacoes);
    }
}