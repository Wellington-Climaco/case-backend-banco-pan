namespace CaseBancoPan.API.Requests.PessoaRequests;

public record CadastrarPessoaRequest(string primeiroNome, string ultimoNome,string endereco,
    string telefone,string email,string dataNascimento);