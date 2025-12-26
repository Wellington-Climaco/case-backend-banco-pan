using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Requests.PessoaRequests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CaseBancoPan.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;
    private readonly IValidator<CadastrarPessoaRequest> _cadastrarRequestValidator;
    private readonly IValidator<AtualizarPessoaRequest> _atualizarRequestValidator;

    public PessoaController(IPessoaService pessoaService,IValidator<CadastrarPessoaRequest> cadastrarRequestValidator,IValidator<AtualizarPessoaRequest> atualizarRequestValidator)
    {
        _pessoaService = pessoaService;
        _cadastrarRequestValidator = cadastrarRequestValidator;
        _atualizarRequestValidator = atualizarRequestValidator;
    }
    
    [HttpPost]
    [Route("/cadastrar")]
    public async Task<IActionResult> Cadastrar(CadastrarPessoaRequest request)
    {
        var validation = await _cadastrarRequestValidator.ValidateAsync(request);
        if(!validation.IsValid)
            return BadRequest(validation.Errors.Select(x=>x.ErrorMessage));

        var result = await _pessoaService.Cadastrar(request);

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

    [HttpPut]
    [Route("/atualizar/")]
    public async Task<IActionResult> Atualizar(AtualizarPessoaRequest request)
    {
        var validation = await _atualizarRequestValidator.ValidateAsync(request);
        if(!validation.IsValid)
            return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
        
        var result = await _pessoaService.Atualizar(request);
        
        if (result.IsSuccess)
            return Ok(result.Value);
        
        if (result.Errors.First().Message.ToLower().Contains("não encontrado"))
            return NotFound(result.Errors.First().Message);
        
        if (result.Errors.First().Message.ToLower().Contains("inválido"))
            return BadRequest(result.Errors.First().Message);
        
        return StatusCode(500);
    }
}