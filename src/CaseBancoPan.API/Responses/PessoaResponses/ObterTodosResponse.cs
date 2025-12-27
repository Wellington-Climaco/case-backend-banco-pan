using Microsoft.Win32;

namespace CaseBancoPan.API.Responses.PessoaResponses
{
    public record ObterTodosRegistrosResponse(int PaginaAtual,int TamanhoPagina,int TotalPaginas,
        int TotalRegistros,bool PossuiPaginaAnterior,bool PossuiPaginaSeguinte, IEnumerable<PessoaResponse> Registros);
    
}
