using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;

namespace ControleGastos.Api.Services;

/// <summary>
/// Contrato de serviço responsável pelas operações de persistência e validação de regras de domínio de movimentações financeiras.
/// </summary>
public interface ITransacaoService
{
    /// <summary>
    /// Processa o registro de uma nova transação assincronamente, aplicando as restrições de negócio vigentes no sistema.
    /// </summary>
    /// <param name="dto">Modelo com os dados necessários para homologação do lançamento.</param>
    /// <returns>Uma tarefa representando a operação assíncrona, contendo a entidade de transação persistida.</returns>
    Task<Transacao> CadastrarAsync(TransacaoCreateDTO dto);

    /// <summary>
    /// Recupera o histórico consolidado de todas as receitas e despesas armazenadas no ecossistema.
    /// </summary>
    /// <returns>Uma coleção iterável de entidades de transação.</returns>
    Task<IEnumerable<Transacao>> ListarTodasAsync();
}
