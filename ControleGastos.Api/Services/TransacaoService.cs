using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class TransacaoService : ITransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService (AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cadastra uma transação aplicando as validações de negócio.
    /// Lança exceção caso as regras sejam violadas.
    /// </summary>

    public async Task<Transacao> CadastrarAsync(TransacaoCreateDTO dto)
    {
        // Regra 1: Verificar se a pessoa existe no banco de dados (RF01 associado à Transação)
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);

        if (pessoa == null)
        {
            throw new Exception("Pessoa não encontrada no sistema.");
        }

        // Regra 2: Menores de 18 anos só podem registrar despesas
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
        {
            throw new ArgumentException("Menores de 18 anos não podem registrar receitas, apenas despesas.");
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

    public async Task<IEnumerable<Transacao>> ListarTodasAsync()
    {
        // Include carrega os dados da Pessoa associada (Eager Loading), 
        // equivalente ao JOIN no SQL.
        return await _context.Transacoes.Include(t => t.Pessoa).ToListAsync();
    }
}