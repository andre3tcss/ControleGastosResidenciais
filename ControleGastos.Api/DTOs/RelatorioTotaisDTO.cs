namespace ControleGastos.Api.DTOs;

public class PessoaTotalDTO
{
    public required string Nome { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    // Propriedade computada: O sistema calcula sozinho, não precisamos setar manualmente.
    public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
}

public class RelatorioGeralDTO
{
    public List<PessoaTotalDTO> Pessoas { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoLiquidoGeral => TotalGeralReceitas - TotalGeralDespesas;
}