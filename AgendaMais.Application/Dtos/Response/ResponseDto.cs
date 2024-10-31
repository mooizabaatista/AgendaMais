namespace AgendaMais.Application.Dtos.Response;

public class ResponseDto
{
    public object? Resultado { get; set; }
    public bool EstaValido { get; set; } = false;
    public string Mensagem { get; set; } = "";
}