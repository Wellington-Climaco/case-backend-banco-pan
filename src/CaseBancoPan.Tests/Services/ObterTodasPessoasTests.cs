using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CaseBancoPan.Tests.Services
{
    public class ObterTodasPessoasTests
    {
        private readonly Mock<IPessoaRepository> _mockRepository = new();
        private readonly IPessoaService _pessoaService;

        public ObterTodasPessoasTests()
        {
            _pessoaService = new PessoaService(_mockRepository.Object, NullLogger<PessoaService>.Instance);
        }

        [Fact]
        public async Task DeveRetornarMensagemDeErroQuandoNenhumRegistroEncontrado()
        {
            //arrange
            string expectedErrorMessage = "Nenhum registro encontrado";
            _mockRepository.Setup(x => x.ObterTodos(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(([],0));

            //act
            var result = await _pessoaService.ObterTodosPaginado(It.IsAny<int>());

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Errors.First().Message);
        }

        [Fact]
        public async Task DeveRetornarRegistrosPaginadosQuandoHaDadosNoBanco()
        {
            //arrange
            int expectedPaginaAtual = 1;
            int expectedTotalRegistros = 4;

            Pessoa[] pessoa =
            {
            new Pessoa("jose", "santos", "rua A", "11970707070", $"email1@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("ana", "souza", "rua B", "11970707070", $"email2@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("maria julia", "cardoso", "rua C", "11970707070", $"email3@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("felipe", "silva", "rua D", "11970707070", $"email4@email.com", new DateTime(2000, 01, 01))
            };

            _mockRepository.Setup(x => x.ObterTodos(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((pessoa.ToList(),4));
            //act
            var result = await _pessoaService.ObterTodosPaginado();

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal(4, result.Value.TotalRegistros);
            Assert.Equal(1, result.Value.PaginaAtual);
            Assert.NotEmpty(result.Value.Registros);
        }
    }
}
