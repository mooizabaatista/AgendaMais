using AgendaMais.Application.AutoMapper;
using AgendaMais.Application.Interfaces;
using AgendaMais.Application.Services;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using AgendaMais.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Permitir qualquer origem
            .AllowAnyMethod() // Permitir qualquer método (GET, POST, etc.)
            .AllowAnyHeader(); // Permitir qualquer cabeçalho
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlite("Data Source=AgendaMais.Infra.db");
});

builder.Services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
builder.Services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IServicoService, ServicoService>();

builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();

builder.Services.AddScoped<IAgendamentoServicoRepository, AgendamentoServicoRepository>();
builder.Services.AddScoped<IAgendamentoServicoService, AgendamentoServicoService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();