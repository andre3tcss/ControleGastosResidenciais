namespace ControleGastos.Api.DTOs;

/// <summary>
/// Objeto de Transferência de Dados utilizado para a criação de uma nova Pessoa
/// Isola a Entidade de domínio de manipulações indevidas vindas da requisição externa.
/// </summary>

public class PessoaCreateDTO 
{
	public required string Nome { get; set; }
	public int idade { get; set; }

}