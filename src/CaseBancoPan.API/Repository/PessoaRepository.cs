using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using Microsoft.EntityFrameworkCore;

namespace CaseBancoPan.API.Repository;

public class PessoaRepository : IPessoaRepository
{
    private readonly DatabaseContext _dbContext;

    public PessoaRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Save(Pessoa pessoa)
    {
        await _dbContext.Pessoas.AddAsync(pessoa);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Pessoa> ObterPorEmail(string email)
    {
        var result = await _dbContext.Pessoas.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
        return result;
    }

    public Task<Pessoa> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }


}