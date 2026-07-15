using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // A rota será: localhost:porta/api/pessoas
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] PessoaCreateDTO dto)
    {
        var pessoa = await _pessoaService.CadastrarAsync(dto);
        // Retorna HTTP 201 (Created)
        return CreatedAtAction(nameof(Cadastrar), new { id = pessoa.Id }, pessoa);
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var pessoas = await _pessoaService.ListarTodasAsync();
        // Retorna HTTP 200 (OK)
        return Ok(pessoas);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _pessoaService.DeletarAsync(id);
        // Retorna HTTP 204 (No Content) - Indica sucesso, mas sem corpo na resposta
        return NoContent();
    }
}