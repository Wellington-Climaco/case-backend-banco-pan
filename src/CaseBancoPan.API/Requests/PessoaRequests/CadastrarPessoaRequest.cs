using CaseBancoPan.API.Entities;

namespace CaseBancoPan.API.Requests.PessoaRequests;

public record CadastrarPessoaRequest(string primeiroNome, string ultimoNome,string endereco,
    string telefone,string email,string dataNascimento);

public static class MapRequestParaEntity
{
    public static Pessoa MapearRequestParaEntity(this CadastrarPessoaRequest request,DateTime dataNascimento)
    {
        return new Pessoa(request.primeiroNome, request.ultimoNome,
            request.endereco,request.telefone,request.email,dataNascimento);
    }
}