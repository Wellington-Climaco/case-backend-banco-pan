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
    [InlineData("emailteste@gmail.com",true)]
    [InlineData("emailinvalido@teste",false)]
    void DeveVerificarSeEmailEValido(string email,bool expectedResult)
    {
        //act
        var result = Pessoa.ValidarEmail(email);

        //assert
        Assert.Equal(expectedResult,result);
    }

    [Theory]
    [InlineData("emailinvalido", "11957631211","Email inválido")]
    [InlineData("email@teste.com", "123","telefone inválido")]
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
}