using AgendaMais.Application.Dtos.Estabelecimento;

namespace AgendaMais.Application.Dtos.Servico;

public class ServicoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";     
    public decimal Preco { get; set; }          
    public int EstabelecimentoId { get; set; }
    public EstabelecimentoDto? Estabelecimento { get; set; } 
}