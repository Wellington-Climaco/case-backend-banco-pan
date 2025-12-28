using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CaseBancoPan.Tests.Services;

public class ObterPorIdTests
{
    private readonly Mock<IPessoaRepository> _mockRepository = new();
    private readonly IPessoaService _pessoaService;

    public ObterPorIdTests()
    {
        _pessoaService = new PessoaService(_mockRepository.Object, NullLogger<PessoaService>.Instance);
    }

    [Fact]
    public async Task DeveRetornarPessoa()
    {
        //arrange
        var pessoa = new Pessoa("primeiroNome", "segundoNome", "rua B", "11970707070",
            "email@email.com", new DateTime(2003, 03, 18));
        
        _mockRepository.Setup(x => x.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(pessoa);
        //act
        var result = await _pessoaService.ObterPorId(Guid.NewGuid());

        //assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Endereco, pessoa.Endereco);
    }
    
    [Fact]
    public async Task DeveRetornarErroQuandoRegistroNaoEncontrado()
    {
        //arrange
        string expectedErrorMessage = "Registro nao encontrado";
        _mockRepository.Setup(x => x.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(null as Pessoa);
        
        //act
        var result = await _pessoaService.ObterPorId(Guid.NewGuid());

        //assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Errors.First().Message);
    }
}