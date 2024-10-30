namespace AgendaMais.Domain.Entities;

public class Estabelecimento
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Endereco { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Email { get; set; } = "";
    public List<Servico> Servicos { get; set; } = new();
    public List<Cliente> Clientes { get; set; } = new();
}