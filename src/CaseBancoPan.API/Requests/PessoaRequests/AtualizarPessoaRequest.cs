namespace CaseBancoPan.API.Requests.PessoaRequests;

public record AtualizarPessoaRequest(Guid Id,string primeiroNome, string ultimoNome,string endereco,
    string telefone,string email);