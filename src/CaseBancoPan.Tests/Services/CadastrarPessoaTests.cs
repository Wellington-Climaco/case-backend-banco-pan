using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CaseBancoPan.Tests.Services;

public class CadastrarPessoaTests
{
    private readonly Mock<IPessoaRepository> _mockRepository = new();
    private readonly IPessoaService _pessoaService;

    public CadastrarPessoaTests()
    {
        _pessoaService = new PessoaService(_mockRepository.Object, NullLogger<PessoaService>.Instance);
    }

    [Fact]
    public async Task DeveCadastrarPessoaQuandoDadosValidos()
    {
       //arrange
       var request = new CadastrarPessoaRequest("primeiroNome", "segundoNome", "rua B", "11970707070",
           "email@email.com", new DateTime(2003,03,18));

       _mockRepository.Setup(x => x.ObterPorEmail("email@email.com")).ReturnsAsync(null as Pessoa);
       
       //act
       var result = await _pessoaService.Cadastrar(request);

       //assert
       Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task DeveRetornarErroQuandoExistirCadastroComEmail()
    {
        //arrange
        string expectedErrorMessage = "Cadastro invalido, email já existente no sistema";
        var request = new CadastrarPessoaRequest("primeiroNome", "segundoNome", "rua B", "11970707070",
            "email@email.com", new DateTime(2003, 03, 18));
        
        var pessoa = new Pessoa("primeiroNome", "segundoNome", "rua B", "11970707070",
            "email@email.com", new DateTime(2003, 03, 18));
        
        _mockRepository.Setup(x => x.ObterPorEmail("email@email.com")).ReturnsAsync(pessoa);
       
        //act
        var result = await _pessoaService.Cadastrar(request);

        //assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage,result.Errors.First().Message);
    }
}