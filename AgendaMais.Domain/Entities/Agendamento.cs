namespace AgendaMais.Domain.Entities;

public class Agendamento
{
    public int Id { get; set; }     
    public DateTime Data { get; set; }         
    public TimeSpan HoraInicio { get; set; }   
    public TimeSpan HoraFim { get; set; }      
    public int ClienteId { get; set; }         
    public Cliente Cliente { get; set; } = new();

    // Adiciona a relação de muitos-para-muitos com `Servico`
    public ICollection<AgendamentoServico> AgendamentoServicos { get; set; }
}