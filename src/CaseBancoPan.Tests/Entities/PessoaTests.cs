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
        
        Pessoa pessoa = new Pessoa("nome", "endereco", "11957631250", "123456", dataNascimentoConverted);
        //act
        var result = pessoa.VerificarMaioridade(hoje);

        //assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("emailteste@gmail.com",true)]
    [InlineData("emailinvalido@teste",false)]
    void DeveVerificarSeEmailEValido(string email,bool expectedResult)
    {
        //arrange
        Pessoa pessoa = new Pessoa("nome", "endereco", "11957631250", email, new DateTime(2000,01,01));
        
        //act
        var result = pessoa.ValidarEmail();

        //assert
        Assert.Equal(expectedResult,result);
    }
}