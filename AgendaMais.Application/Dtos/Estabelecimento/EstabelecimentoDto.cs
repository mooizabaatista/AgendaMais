﻿namespace AgendaMais.Application.Dtos.Estabelecimento;

public class EstabelecimentoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Endereco { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Email { get; set; } = "";
}