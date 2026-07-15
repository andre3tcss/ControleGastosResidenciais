using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    // Injeção de dependência do contexto de banco de dados
    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cadastra uma nova pessoa mapeando o DTO para a Entidade.
    /// </summary>

    public async Task<Pessoa> CadastrarAsync(PessoaCreateDTO dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.idade
        };

        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync(); // Efetiva o comando INSERT no banco

        return pessoa;
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>

    public async Task<IEnumerable<Pessoa>> ListarTodasAsync()
    {
        return await _context.Pessoas.ToListAsync(); // Efetiva o comando SELECT
    }

    /// <summary>
    /// Deleta uma pessoa pelo ID. 
    /// Devido à configuração do Entity Framework (Cascade Delete), 
    /// as transações associadas serão removidas automaticamente (RF04).
    /// </summary>

    public async Task DeletarAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync(); // Efetiva o comando DELETE
        }
    }
}