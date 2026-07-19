namespace ControleGastos.Api.Entities;

/// <summary>
/// Representa a entidade de domínio de movimentações e lançamentos financeiros do sistema.
/// </summary>
public class Transacao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// Identificador de integridade referencial que estabelece o vínculo com a entidade Pessoa.
    /// </summary>
    public Guid PessoaId { get; set; }

    /// <summary>
    /// Propriedade estrutural para carregamento e resolução do relacionamento de dependência via ORM.
    /// </summary>
    public Pessoa? Pessoa { get; set; }
}
