using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

public interface IRelatorioService
{
    Task<RelatorioGeralDTO> ObterTotaisGeraisAsync();
}