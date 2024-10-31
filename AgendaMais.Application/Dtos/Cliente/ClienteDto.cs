using AgendaMais.Application.Dtos.Estabelecimento;

namespace AgendaMais.Application.Dtos.Cliente;

public class ClienteDto
{
    public int Id { get; set; }    
    public string Nome { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Email { get; set; } = "";
    public int EstabelecimentoId { get; set; }
    public EstabelecimentoDto? Estabelecimento { get; set; }
}