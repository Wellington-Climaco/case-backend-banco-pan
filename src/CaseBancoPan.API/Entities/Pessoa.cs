using System.Text.RegularExpressions;

namespace CaseBancoPan.API.Entities;

public class Pessoa
{
    public Pessoa(string nome, string endereco, string telefone,string email, DateTime dataNascimento)
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        DataNascimento = dataNascimento;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    
    
}