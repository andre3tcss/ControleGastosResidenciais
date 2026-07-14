namespace ControleGastos.Api.Entities;

public class Pessoa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nome { get; set; }
    public int Idade { get; set; }

    // Propriedade de Navegação (Relacionamento 1:N)
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}