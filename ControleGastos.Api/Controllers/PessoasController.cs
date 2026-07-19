using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    /// <summary>
    /// Registra uma nova entidade de pessoa na base de dados.
    /// </summary>
    /// <param name="dto">Payload contendo os dados para a persistência da pessoa.</param>
    /// <returns>A entidade persistida com o respectivo identificador único gerado.</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] PessoaCreateDTO dto)
    {
        var pessoa = await _pessoaService.CadastrarAsync(dto);
        return CreatedAtAction(nameof(Cadastrar), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Recupera a coleção completa de pessoas cadastradas no sistema.
    /// </summary>
    /// <returns>Coleção de DTOs representando as pessoas persistidas.</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var pessoas = await _pessoaService.ListarTodasAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Remove uma pessoa do sistema e dispara a exclusão em cascata de suas respectivas transações.
    /// </summary>
    /// <param name="id">Identificador único (GUID) da pessoa a ser removida.</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _pessoaService.DeletarAsync(id);
        return NoContent();
    }
}