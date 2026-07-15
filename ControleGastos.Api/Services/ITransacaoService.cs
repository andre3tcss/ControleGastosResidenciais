using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;

namespace ControleGastos.Api.Services;

public interface ITransacaoService
{
    Task<Transacao> CadastrarAsync(TransacaoCreateDTO dto);
    Task<IEnumerable<Transacao>> ListarTodasAsync();
}