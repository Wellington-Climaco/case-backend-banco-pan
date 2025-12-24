using CaseBancoPan.API.Entities;

namespace CaseBancoPan.API.Interface;

public interface IPessoaRepository
{
    Task Save(Pessoa pessoa);
    Task<Pessoa> ObterPorId(Guid id);
    Task<Pessoa> ObterPorEmail(string email);
    Task Remover(Pessoa pessoa);
}