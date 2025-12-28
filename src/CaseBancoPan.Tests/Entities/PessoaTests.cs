using CaseBancoPan.API.Entities;

namespace CaseBancoPan.Tests.Entities;

public class PessoaTests
{
    [Theory]
    [InlineData("15/01/2000",false)]
    [InlineData("01/01/2000",true)]
    void DeveVerificarMaioridade(string dataNascimento, bool expectedResult)
    {
        //arrange
        DateTime dataNascimentoConverted = DateTime.Parse(dataNascimento);
        DateTime hoje = new DateTime(2018,01,01);
        
        //act
        var result = Pessoa.VerificarMaioridade(hoje,dataNascimentoConverted);

        //assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("emailinvalido", "11957631211","Email invalido")]
    [InlineData("email@teste.com", "123","telefone invalido")]
    public void DeveLancarExceptionComMensagemDeErroQuandoArgumentosDoConstrutorInvalidos(string email, string telefone,string errorMessage)
    {
        // arrange
        Action act = () =>
            new Pessoa(
                "nome",
                "sobrenome",
                "endereco",
                telefone,
                email,
                new DateTime(2000,01,01) // menor de idade
            );

        // act & assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal(errorMessage, exception.Message);
    }

    [Fact]
    public void DeveCriarPessoaQuandoDadosSaoValidos()
    {
        //arrange
        var pessoa = new Pessoa(
            "nome",
            "sobrenome",
            "endereco",
            "11957631250",
            "email@teste.com",
            DateTime.Now.AddYears(-20)
        );

        // assert
        Assert.NotNull(pessoa);
    }

    [Fact]
    public void DeveRetornarNomeCompleto()
    {
        //arrange
        string expectedNomeCompleto = "primeiro segundo";
        var pessoa = new Pessoa("primeiro", "segundo", "endereco", "11970707070", "email@email.com",
            new DateTime(2000, 01, 01));
        //act
        var result = pessoa.ObterNomeCompleto();

        //assert
        Assert.Equal(expectedNomeCompleto, result);
    }

    [Fact]
    public void DeveAlterarNomeQuandoDadosSaoValidos()
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        pessoa.AlterarNome("Carlos", "Souza");

        // assert
        Assert.Equal("Carlos", pessoa.PrimeiroNome);
        Assert.Equal("Souza", pessoa.UltimoNome);
    }

    [Theory]
    [InlineData("", "Souza")]
    [InlineData("Jo", "Souza")]
    [InlineData("Carlos", "")]
    [InlineData("Carlos", "So")]
    public void DeveLancarExceptionAoAlterarNomeInvalido(string primeiroNome, string ultimoNome)
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        Action act = () => pessoa.AlterarNome(primeiroNome, ultimoNome);

        // assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Nome invalido", exception.Message);
    }

    [Fact]
    public void DeveAlterarEnderecoQuandoEnderecoValido()
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        pessoa.AlterarEndereco("Rua Nova, 123");

        // assert
        Assert.Equal("Rua Nova, 123", pessoa.Endereco);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void DeveLancarExceptionAoAlterarEnderecoInvalido(string endereco)
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        Action act = () => pessoa.AlterarEndereco(endereco);

        // assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Endereço invalido", exception.Message);
    }

    [Fact]
    public void DeveAlterarTelefoneQuandoTelefoneValido()
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        pessoa.AlterarTelefone("11988887777");

        // assert
        Assert.Equal("11988887777", pessoa.Telefone);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("abcdefghijk")]
    [InlineData("")]
    public void DeveLancarExceptionAoAlterarTelefoneInvalido(string telefone)
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        Action act = () => pessoa.AlterarTelefone(telefone);

        // assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("telefone invalido", exception.Message);
    }

    [Fact]
    public void DeveAlterarEmailQuandoEmailValido()
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        pessoa.AlterarEmail("novo@email.com");

        // assert
        Assert.Equal("novo@email.com", pessoa.Email);
    }

    [Theory]
    [InlineData("emailinvalido")]
    [InlineData("email@teste")]
    public void DeveLancarExceptionAoAlterarEmailInvalido(string email)
    {
        // arrange
        var pessoa = new Pessoa(
            "Joao",
            "Silva",
            "Rua A",
            "11999999999",
            "joao@email.com",
            DateTime.Now.AddYears(-20)
        );

        // act
        Action act = () => pessoa.AlterarEmail(email);

        // assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Email invalido", exception.Message);
    }

    
}