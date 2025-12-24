using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace CaseBancoPan.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }
    
    [HttpPost]
    [Route("/cadastrar")]
    public async Task<IActionResult> Cadastrar(CadastrarPessoaRequest pessoa)
    {
        var result = await _pessoaService.Cadastrar(pessoa);
      
        if(result.IsSuccess)
            return Created($"/obterById/{result.Value.Id}", result.Value);

        if (result.Errors.First().Message.ToLower().Contains("inválido"))
            return BadRequest(result.Errors.First().Message);

        return StatusCode(500);
    }

    [HttpGet]
    [Route("/obterPorId/{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
    {
        var result = await _pessoaService.ObterPorId(id);
        if(result.IsSuccess)
            return Ok(result.Value);
        
        if (result.Errors.First().Message.ToLower().Contains("não encontrado"))
            return NotFound(result.Errors.First().Message);
        
        return StatusCode(500);
    }
    
    [HttpDelete]
    [Route("/remover/{id}")]
    public async Task<IActionResult> RemoverPorId(Guid id)
    {
        var result = await _pessoaService.Remover(id);
        if (result.IsSuccess)
            return Ok();
        
        if (result.Errors.First().Message.ToLower().Contains("não encontrado"))
            return NotFound(result.Errors.First().Message);
        
        return StatusCode(500);
    }
}