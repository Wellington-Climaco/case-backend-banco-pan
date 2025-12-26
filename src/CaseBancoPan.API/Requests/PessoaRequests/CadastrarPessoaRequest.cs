using CaseBancoPan.API.Entities;

namespace CaseBancoPan.API.Requests.PessoaRequests;

public record CadastrarPessoaRequest(string primeiroNome, string ultimoNome,string endereco,
    string telefone,string email,DateTime dataNascimento);

public static class MapRequestParaEntity
{
    public static Pessoa MapearRequestParaEntity(this CadastrarPessoaRequest request)
    {
        return new Pessoa(request.primeiroNome, request.ultimoNome,
            request.endereco,request.telefone,request.email,request.dataNascimento);
    }
}