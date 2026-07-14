namespace ControleGastos.Api.Entities;

public class Transacao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Descricao { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }

    // Chave Estrangeira
    public Guid PessoaId { get; set; }

    // Propriedade de Navegação
    public Pessoa? Pessoa { get; set; }
}