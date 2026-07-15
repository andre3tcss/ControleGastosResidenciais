using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;

namespace ControleGastos.Api.Services;

/// <summary>
/// Contrato que define as operações de negócio para a entidade Pessoa.
/// Facilita a injeção de dependência e a criação de mocks para testes unitários.
/// </summary>
public interface IPessoaService 
{
	Task<Pessoa> CadastrarAsync(PessoaCreateDTO dto);
	Task<IEnumerable<Pessoa>> ListarTodasAsync();
	Task DeletarAsync(Guid id);
}