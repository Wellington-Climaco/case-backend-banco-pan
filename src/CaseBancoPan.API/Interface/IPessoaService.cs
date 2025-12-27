using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Responses.PessoaResponses;
using FluentResults;

namespace CaseBancoPan.API.Interface;

public interface IPessoaService
{
    Task<Result<PessoaResponse>> Cadastrar(CadastrarPessoaRequest request);
    Task<Result<PessoaResponse>> ObterPorId(Guid id);
    Task<Result> Remover(Guid id);
    Task<Result<PessoaResponse>> Atualizar(AtualizarPessoaRequest request);
    Task<Result<ObterTodosRegistrosResponse>> ObterTodosPaginado(int pagina,int tamanhoPagina = 5);
}