using AgendaMais.Application.Dtos.Cliente;
using AgendaMais.Application.Dtos.Servico;

namespace AgendaMais.Application.Dtos.Agendamento;

public class AgendamentoDto
{
    public int Id { get; set; }     
    public DateTime Data { get; set; }         
    public TimeSpan HoraInicio { get; set; }   
    public TimeSpan HoraFim { get; set; }
    public bool Concluido { get; set; }
    public bool Cancelado { get; set; }
    public int ClienteId { get; set; }         
    public ClienteDto? Cliente { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ServicoDto>? Servicos { get; set; }
}