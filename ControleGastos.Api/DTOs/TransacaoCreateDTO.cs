using ControleGastos.Api.Entities;

namespace ControleGastos.Api.DTOs;

/// <summary>
/// Objeto de Transferência de Dados para cadastro de Transações.
/// </summary>

public class TransacaoCreateDTO 
{
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid PessoaId { get; set; }
}