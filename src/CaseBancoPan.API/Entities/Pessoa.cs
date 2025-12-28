using System.Text.RegularExpressions;

namespace CaseBancoPan.API.Entities;

public class Pessoa
{
    public Pessoa(string primeiroNome,string ultimoNome, string endereco, string telefone,string email, DateTime dataNascimento)
    {
        if (!ValidarEmail(email))
            throw new ArgumentException("Email invalido");
                
        if(!ValidarTelefone(telefone))   
            throw new ArgumentException("telefone invalido");

        if (!VerificarMaioridade(DateTime.Now, dataNascimento))
            throw new ArgumentException("invalido, pessoa deve ser maior de idade");
        
        if(!ValidarEndereco(endereco))
            throw new ArgumentException("Endereço invalido");
        
        if(!ValidarNome(primeiroNome,ultimoNome))
            throw new ArgumentException("Nome invalido");
        
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        DataNascimento = dataNascimento;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string PrimeiroNome { get; private set; }
    public string UltimoNome { get; private set; }
    public string Endereco { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataNascimento { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; private set; }

    public static bool VerificarMaioridade(DateTime hoje,DateTime dataNascimento) => dataNascimento.AddYears(18) <= hoje;
    private static bool ValidarEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    private static bool ValidarTelefone(string telefone) => !string.IsNullOrWhiteSpace(telefone) && telefone.Length == 11 && telefone.All(char.IsDigit);
    private static bool ValidarEndereco(string endereco) => !string.IsNullOrWhiteSpace(endereco);
    private static bool ValidarNome(string primeiroNome, string ultimoNome)
    {
        return !string.IsNullOrWhiteSpace(primeiroNome) && primeiroNome.Length >= 3 &&
               !string.IsNullOrWhiteSpace(ultimoNome) && ultimoNome.Length >= 3;
    }
    
    public string ObterNomeCompleto()
    {
        return $"{PrimeiroNome} {UltimoNome}";
    }
    public void SetarUpdatedAt() => UpdatedAt = DateTime.Now;
    public void AlterarNome(string primeiroNome, string ultimoNome)
    {
        if(!ValidarNome(primeiroNome,ultimoNome))
            throw new ArgumentException("Nome invalido");
        
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
    }
    public void AlterarEndereco(string endereco)
    {
        if(!ValidarEndereco(endereco))
            throw new ArgumentException("Endereço invalido");
        Endereco = endereco;
    }
    public void AlterarTelefone(string telefone)
    {
        if(!ValidarTelefone(telefone))   
            throw new ArgumentException("telefone invalido");
        
        Telefone = telefone;
    }
    public void AlterarEmail(string email)
    { 
        if (!ValidarEmail(email))
            throw new ArgumentException("Email invalido");
        Email = email;
    }
    
}