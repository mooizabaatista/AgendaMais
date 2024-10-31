namespace AgendaMais.Application.Dtos.Agendamento;

public class AgendamentoCommandDto
{
    public DateTime Data { get; set; }         
    public TimeSpan HoraInicio { get; set; }   
    public TimeSpan HoraFim { get; set; }
    public bool Concluido { get; set; }
    public bool Cancelado { get; set; }
    public int ClienteId { get; set; }
    public decimal ValorTotal { get; set; }
    public List<int>? Servicos { get; set; }
}