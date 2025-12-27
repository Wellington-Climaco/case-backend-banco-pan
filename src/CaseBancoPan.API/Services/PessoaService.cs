using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Responses.PessoaResponses;
using FluentResults;
using System.Globalization;

namespace CaseBancoPan.API.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _repository;
    private readonly ILogger<PessoaService> _logger;
    public PessoaService(IPessoaRepository repository, ILogger<PessoaService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<PessoaResponse>> Cadastrar(CadastrarPessoaRequest request)
    {
        try
        {
            var cadastro = await _repository.ObterPorEmail(request.email);
            if (cadastro is not null)
                return Result.Fail("Cadastro inválido, email já existente no sistema");

            var pessoa = request.MapearRequestParaEntity();

                await _repository.Save(pessoa);
           
            var response = pessoa.MapearEntityParaResponse();
            
            return Result.Ok(response);
        }
        catch(ArgumentException ex)
        {
            _logger.LogWarning(ex.ToString());
            return Result.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<PessoaResponse>> ObterPorId(Guid id)
    {
        try
        {
            var pessoa = await _repository.ObterPorId(id);
            if (pessoa is null)
                return Result.Fail("Registro não encontrado");
            
            var response = new PessoaResponse(pessoa.Id.ToString(),pessoa.ObterNomeCompleto(),
                pessoa.Email,pessoa.Telefone,pessoa.Endereco,pessoa.DataNascimento.ToString("dd/MM/yyyy"));

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("erro inesperado: {Exception}", ex.ToString());
            return Result.Fail(ex.ToString());
        }
    }

    public async Task<Result> Remover(Guid id)
    {
        try
        {
            var pessoa = await _repository.ObterPorId(id);
            if (pessoa is null)
                return Result.Fail("Registro não encontrado");

            await _repository.Remover(pessoa);
            
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("erro inesperado: {Exception}", ex.ToString());
            return Result.Fail(ex.ToString());
        }
    }

    public async Task<Result<PessoaResponse>> Atualizar(AtualizarPessoaRequest request)
    {
        try
        {
            var pessoa = await _repository.ObterPorId(request.Id);
            if (pessoa is null)
                return Result.Fail("registro não encontrado");
            
            pessoa.AlterarNome(request.primeiroNome, request.ultimoNome);
            pessoa.AlterarEndereco(request.endereco);
            pessoa.AlterarEmail(request.email);
            pessoa.AlterarTelefone(request.telefone);

            pessoa.SetarUpdatedAt();
            
            await _repository.Atualizar(pessoa);

            var response = pessoa.MapearEntityParaResponse();
            return Result.Ok(response);
        }
        catch(ArgumentException ex)
        {
            _logger.LogWarning(ex.ToString());
            return Result.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("erro inesperado: {Exception}", ex.ToString());
            return Result.Fail(ex.ToString());
        }
    }

    public async Task<Result<ObterTodosRegistrosResponse>> ObterTodosPaginado(int pagina,int tamanhoPagina = 5)
    {
        try
        {
            var result = await _repository.ObterTodos(tamanhoPagina,pagina);
            if (result.pessoas.Count == 0)
                return Result.Fail("Nenhum registro encontrado");

            var registros = result.pessoas.Select(x => x.MapearEntityParaResponse());

            int totalPaginas = (int)Math.Ceiling((double)result.totalRegistros / tamanhoPagina);
            bool possuiPaginaAnterior = pagina > 1;
            bool possuiPaginaSeguinte = pagina < totalPaginas;

            var response = new ObterTodosRegistrosResponse(pagina, tamanhoPagina, totalPaginas,result.totalRegistros,possuiPaginaAnterior,possuiPaginaSeguinte,registros);

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return Result.Fail(ex.ToString());
        }        
    }
}