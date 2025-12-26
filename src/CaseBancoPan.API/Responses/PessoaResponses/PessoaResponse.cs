using CaseBancoPan.API.Entities;

namespace CaseBancoPan.API.Responses.PessoaResponses;

public record PessoaResponse(string Id,string Nome,string Email,string Telefone,string Endereco,string DataNascimento);

public static class MapEntityParaResponse
{
    public static PessoaResponse MapearEntityParaResponse(this Pessoa pessoa)
    {
        return new PessoaResponse(pessoa.Id.ToString(),pessoa.ObterNomeCompleto(),
            pessoa.Email,pessoa.Telefone,pessoa.Endereco,pessoa.DataNascimento.ToString("dd/MM/yyyy"));
    }
}