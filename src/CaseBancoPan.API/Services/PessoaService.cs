using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Responses.PessoaResponses;
using FluentResults;

namespace CaseBancoPan.API.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _repository;
    public PessoaService(IPessoaRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<PessoaResponse>> Cadastrar(CadastrarPessoaRequest request)
    {
        try
        {
            var dataNascimentoConvertida= DateTime.TryParse(request.dataNascimento, out DateTime dataNascimento);
            if (!dataNascimentoConvertida)
                Result.Ok();
            
            var pessoa = new Pessoa(request.primeiroNome, request.ultimoNome,
                request.endereco,request.telefone,request.email,dataNascimento);
        
            await _repository.Save(pessoa);

            var response = new PessoaResponse(pessoa.Id.ToString(),$"{pessoa.PrimeiroNome } {pessoa.UltimoNome}",
                pessoa.Email,pessoa.Telefone,pessoa.Endereco,pessoa.DataNascimento.ToString("dd/MM/yyyy"));
            
            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}