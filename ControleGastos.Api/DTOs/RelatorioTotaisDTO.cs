namespace ControleGastos.Api.DTOs;

/// <summary>
/// Projeção consolidada do balanço financeiro individual por morador.
/// </summary>
public class PessoaTotalDTO
{
    public required string Nome { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    /// <summary>
    /// Regra de negócio: Expressão embutida para cálculo automatizado do saldo líquido disponível do indivíduo.
    /// </summary>
    public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
}

/// <summary>
/// Modelo de transferência de dados (DTO) contendo o balanço global e auditoria do sistema residencial.
/// </summary>
public class RelatorioGeralDTO
{
    public List<PessoaTotalDTO> Pessoas { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }

    /// <summary>
    /// Regra de negócio: Expressão embutida para consolidação final e fechamento do balanço líquido de todas as contas.
    /// </summary>
    public decimal SaldoLiquidoGeral => TotalGeralReceitas - TotalGeralDespesas;
}
