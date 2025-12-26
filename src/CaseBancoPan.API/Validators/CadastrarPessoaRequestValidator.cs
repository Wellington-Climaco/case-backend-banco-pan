using CaseBancoPan.API.Requests.PessoaRequests;
using FluentValidation;

namespace CaseBancoPan.API.Validators;

public class CadastrarPessoaRequestValidator : AbstractValidator<CadastrarPessoaRequest>
{
    public CadastrarPessoaRequestValidator()
    {
        RuleFor(v => v.primeiroNome)
            .NotEmpty().WithMessage("Preencha o campo primeiroNome")
            .MinimumLength(3).WithMessage("Campo primeiro nome deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Campo primeiro nome deve ter no mínimo 3 caracteres");
        
        RuleFor(v => v.ultimoNome)
            .NotEmpty().WithMessage("Preencha o campo ultimoNome.")
            .MinimumLength(3).WithMessage("O ultimoNome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O ultimoNome deve ter no máximo 100 caracteres.");

        RuleFor(v => v.endereco)
            .NotEmpty().WithMessage("Preencha o campo endereco.")
            .MaximumLength(200).WithMessage("O endereco deve ter no máximo 200 caracteres.");

        RuleFor(v => v.telefone)
            .NotEmpty().WithMessage("Preencha o campo telefone.")
            .Length(11).WithMessage("O telefone deve ter 11 caracteres.");

        RuleFor(v => v.email)
            .NotEmpty().WithMessage("Preencha o campo email.")
            .EmailAddress().WithMessage("Email inválido.");
        
        RuleFor(v => v.dataNascimento)
            .NotEmpty().WithMessage("Preencha o campo dataNascimento.");
    }
}