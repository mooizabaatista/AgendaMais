namespace AgendaMais.Application.Dtos.Servico;

public class ServicoCommandDto
{
    public string Nome { get; set; } = "";     
    public decimal Preco { get; set; }          
    public int EstabelecimentoId { get; set; }
}