namespace AgendaMais.Application.Dtos.Cliente;

public class ClienteCommandDto
{
    public string Nome { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Email { get; set; } = "";
    public int EstabelecimentoId { get; set; }
}