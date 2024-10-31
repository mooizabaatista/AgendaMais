using AgendaMais.Application.Dtos.Agendamento;
using AgendaMais.Application.Dtos.Servico;

namespace AgendaMais.Application.Dtos.AgendamentoServico;

public class AgendamentoServicoDto
{
    public int AgendamentoId { get; set; }
    public AgendamentoDto? Agendamento { get; set; }

    public int ServicoId { get; set; }
    public ServicoDto? Servico { get; set; }
}