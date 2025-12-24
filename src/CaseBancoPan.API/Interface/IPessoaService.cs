using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Responses.PessoaResponses;
using FluentResults;

namespace CaseBancoPan.API.Interface;

public interface IPessoaService
{
    Task<Result<PessoaResponse>> Cadastrar(CadastrarPessoaRequest request);
    Task<Result<PessoaResponse>> ObterPorId(Guid id);
}