using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using CaseBancoPan.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CaseBancoPan.Tests.Services;

public class AtualizarPessoaTests
{
    private readonly Mock<IPessoaRepository> _mockRepository = new();
    private readonly IPessoaService _pessoaService;

    public AtualizarPessoaTests()
    {
        _pessoaService = new PessoaService(_mockRepository.Object, NullLogger<PessoaService>.Instance);
    }

    [Fact]
    public async Task DeveAtualizarPessoaQuandoDadosValidos()
    {
        // arrange
        var pessoa = new Pessoa("nomeAntigo", "sobrenomeAntigo", "enderecoAntigo",
            "11970707070", "email@email.com", new DateTime(2003, 03, 18));

        var request = new AtualizarPessoaRequest(
            pessoa.Id,
            "novoNome",
            "novoSobrenome",
            "novoEndereco",
            "11999999999",
            "novoemail@email.com"
        );

        _mockRepository
            .Setup(x => x.ObterPorId(pessoa.Id))
            .ReturnsAsync(pessoa);

        // act
        var result = await _pessoaService.Atualizar(request);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Equal("novoNome novoSobrenome", result.Value.Nome);
        Assert.Equal("novoEndereco", result.Value.Endereco);

        _mockRepository.Verify(x => x.Atualizar(pessoa), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarErroQuandoRegistroNaoEncontrado()
    {
        // arrange
        string expectedErrorMessage = "registro nao encontrado";

        var request = new AtualizarPessoaRequest(
            Guid.NewGuid(),
            "nome",
            "sobrenome",
            "endereco",
            "11970707070",
            "email@email.com"
        );

        _mockRepository
            .Setup(x => x.ObterPorId(request.Id))
            .ReturnsAsync(null as Pessoa);

        // act
        var result = await _pessoaService.Atualizar(request);

        // assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Errors.First().Message);
        _mockRepository.Verify(x => x.Atualizar(It.IsAny<Pessoa>()), Times.Never);
    }

    [Fact]
    public async Task DeveRetornarErroQuandoArgumentExceptionForLancada()
    {
        // arrange
        string expectedErrorMessage = "Email invalido";

        var pessoa = new Pessoa("nome", "sobrenome", "endereco",
            "11970707070", "email@email.com", new DateTime(2003, 03, 18));

        var request = new AtualizarPessoaRequest(
            pessoa.Id,
            "nome",
            "sobrenome",
            "endereco",
            "11970707070",
            "email-invalido"
        );

        _mockRepository
            .Setup(x => x.ObterPorId(request.Id))
            .ReturnsAsync(pessoa);

        // act
        var result = await _pessoaService.Atualizar(request);

        // assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Errors.First().Message);
        _mockRepository.Verify(x => x.Atualizar(It.IsAny<Pessoa>()), Times.Never);
    }
}