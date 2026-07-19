using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

/// <summary>
/// Orquestrador de domínio responsável pelas regras de negócio, validações de integridade e persistência de lançamentos financeiros.
/// </summary>
public class TransacaoService : ITransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Valida e processa as regras de negócio vigentes para a homologação e persistência de uma movimentação financeira.
    /// </summary>
    /// <param name="dto">Payload contendo as especificações do lançamento financeiro.</param>
    /// <returns>A entidade de transação devidamente indexada e salva na base de dados.</returns>
    /// <exception cref="InvalidOperationException">Lançada caso o vínculo referencial com o morador seja inválido.</exception>
    /// <exception cref="ArgumentException">Lançada caso haja violação nas restrições de conformidade de perfil por idade.</exception>
    public async Task<Transacao> CadastrarAsync(TransacaoCreateDTO dto)
    {
        // Garante a integridade referencial validando a existência prévia da entidade associada na base de dados
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);

        if (pessoa == null)
        {
            throw new InvalidOperationException("Operação inválida: O identificador de morador fornecido não corresponde a um registro ativo.");
        }

        // Regra de governança de domínio: Restringe o fluxo de crédito (Receitas) baseado na classificação legal de maioridade do perfil
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
        {
            throw new ArgumentException("Violação de conformidade: Contas associadas a menores de 18 anos possuem restrição para lançamentos de receita.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            PessoaId = dto.PessoaId
        };

        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }

    /// <summary>
    /// Recupera o histórico consolidado de transações, resolvendo as dependências de perfil associadas.
    /// </summary>
    /// <returns>Coleção iterável de transações financeiras com seus respectivos vínculos de domínio.</returns>
    public async Task<IEnumerable<Transacao>> ListarTodasAsync()
    {
        // Resolve a associação estrutural com a entidade de apoio para exibição completa dos dados na interface
        return await _context.Transacoes.Include(t => t.Pessoa).ToListAsync();
    }
}
