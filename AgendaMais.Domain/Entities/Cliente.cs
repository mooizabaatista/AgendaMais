namespace AgendaMais.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }    
    public string Nome { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Email { get; set; } = "";
    public int EstabelecimentoId { get; set; }
    public Estabelecimento? Estabelecimento { get; set; } 

    // Lista de agendamentos relacionados ao cliente
    public List<Agendamento>? Agendamentos { get; set; } 
}