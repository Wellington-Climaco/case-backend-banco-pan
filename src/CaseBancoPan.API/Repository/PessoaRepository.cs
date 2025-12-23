using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;

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

    public Task<Pessoa> ObterPorEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Pessoa> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }


}