using System.Text.RegularExpressions;

namespace CaseBancoPan.API.Entities;

public class Pessoa
{
    public Pessoa(string primeiroNome,string ultimoNome, string endereco, string telefone,string email, DateTime dataNascimento)
    {
        if (!ValidarEmail(email))
            throw new ArgumentException("Email inválido");
                
        if(!ValidarTelefone(telefone))   
            throw new ArgumentException("telefone inválido");

        if (!VerificarMaioridade(DateTime.Now, dataNascimento))
            throw new ArgumentException("inválido, pessoa deve ser maior de idade");
        
        if(!ValidarEndereco(endereco))
            throw new ArgumentException("Endereço inválido");
        
        if(!ValidarNome(primeiroNome,ultimoNome))
            throw new ArgumentException("Nome inválido");
        
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        DataNascimento = dataNascimento;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public string Endereco { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public static bool VerificarMaioridade(DateTime hoje,DateTime dataNascimento) => dataNascimento.AddYears(18) <= hoje;
    public static bool ValidarEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    public static bool ValidarTelefone(string telefone) => !string.IsNullOrWhiteSpace(telefone) && telefone.Length == 11 && telefone.All(char.IsDigit);
    public static bool ValidarEndereco(string endereco) => !string.IsNullOrWhiteSpace(endereco);
    public static bool ValidarNome(string primeiroNome, string ultimoNome)
    {
        return !string.IsNullOrWhiteSpace(primeiroNome) && primeiroNome.Length >= 3 &&
               !string.IsNullOrWhiteSpace(ultimoNome) && ultimoNome.Length >= 3;
    }
    public string ObterNomeCompleto()
    {
        return $"{PrimeiroNome} {UltimoNome}";
    }
    
}