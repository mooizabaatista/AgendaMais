using AgendaMais.Application.Dtos.Agendamento;
using FluentValidation;

namespace AgendaMais.Application.Validations;

public class AgendamentoValidation : AbstractValidator<AgendamentoCommandDto>
{
    public AgendamentoValidation()
    {
        
        RuleFor(x => x.Data)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe a data do agendamento");
        
        RuleFor(x => x.Servicos)
            .NotNull()
            .WithMessage("Informe algum serviço para o agendamento");
        
        RuleFor(x => x.HoraInicio)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe a hora de inicio");
        
        RuleFor(x => x.HoraFim)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe a hora de término");
        
        RuleFor(x => x.ClienteId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe o cliente do agendamento");

        RuleFor(x => x.HoraInicio)
            .LessThan(y => y.HoraFim)
            .WithMessage("A hora de início do agendamento deve ser menor que a hora final.");

        RuleFor(x => x.HoraFim)
            .GreaterThan(y => y.HoraInicio)
            .WithMessage("A hora de término do agendamento deve ser maior que a hora de início.");
    }
}