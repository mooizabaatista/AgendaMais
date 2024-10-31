using AgendaMais.Application.Dtos.Servico;
using FluentValidation;

namespace AgendaMais.Application.Validations;

public class ServicoValidation : AbstractValidator<ServicoCommandDto>
{
    public ServicoValidation()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("Informe o nome")
            .NotEmpty().WithMessage("Informe o nome")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres");
        
        RuleFor(x => x.EstabelecimentoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Informe o estabelecimento");
        
        RuleFor(x => x.Preco)
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que 0");
    }
}