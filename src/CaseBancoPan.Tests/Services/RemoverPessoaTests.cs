using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CaseBancoPan.Tests.Services;

public class RemoverPessoaTests
{
    private readonly Mock<IPessoaRepository> _mockRepository = new();
    private readonly IPessoaService _pessoaService;

    public RemoverPessoaTests()
    {
        _pessoaService = new PessoaService(_mockRepository.Object, NullLogger<PessoaService>.Instance);
    }

    [Fact]
    public async Task DeveRemoverPessoaQuandoRegistroExistir()
    {
        // arrange
        var pessoa = new Pessoa("primeiroNome", "segundoNome", "rua B", "11970707070",
            "email@email.com", new DateTime(2003, 03, 18));

        _mockRepository
            .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(pessoa);

        // act
        var result = await _pessoaService.Remover(Guid.NewGuid());

        // assert
        Assert.True(result.IsSuccess);
        _mockRepository.Verify(x => x.Remover(pessoa), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarErroQuandoRegistroNaoEncontrado()
    {
        // arrange
        string expectedErrorMessage = "Registro não encontrado";

        _mockRepository
            .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync(null as Pessoa);

        // act
        var result = await _pessoaService.Remover(Guid.NewGuid());

        // assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Errors.First().Message);
        _mockRepository.Verify(x => x.Remover(It.IsAny<Pessoa>()), Times.Never);
    }
}