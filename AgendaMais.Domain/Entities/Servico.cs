namespace AgendaMais.Domain.Entities;

public class Servico
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";     
    public decimal Preco { get; set; }          
    public int EstabelecimentoId { get; set; }
    public Estabelecimento Estabelecimento { get; set; } = new();

    public ICollection<AgendamentoServico> AgendamentoServicos { get; set; }
}