namespace ControleGastos.Api.Entities;

/// <summary>
/// Define os tipos de lançamentos financeiros permitidos pelo domínio da aplicação.
/// </summary>
public enum TipoTransacao
{
    /// <summary>
    /// Identifica saídas financeiras e fluxos de débito no balanço residencial.
    /// </summary>
    Despesa = 0,

    /// <summary>
    /// Identifica entradas financeiras e fluxos de crédito no balanço residencial.
    /// </summary>
    Receita = 1
}