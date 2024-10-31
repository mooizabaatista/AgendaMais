using AgendaMais.Application.Dtos.Cliente;
using FluentValidation;

namespace AgendaMais.Application.Validations;

public class ClienteValidation : AbstractValidator<ClienteCommandDto>
{
    public ClienteValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome")
            .NotNull().WithMessage("Informe o nome")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Informe o telefone")
            .NotNull().WithMessage("Informe o telefone")
            .MaximumLength(14).WithMessage("O telefone deve conter 14 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Informe o email")
            .NotNull().WithMessage("Informe o email")
            .EmailAddress().WithMessage("Informe um email válido")
            .MaximumLength(150).WithMessage("O email deve conter no máximo 150 caracteres");

        RuleFor(x => x.EstabelecimentoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Informe o estabelecimento");
    }
}