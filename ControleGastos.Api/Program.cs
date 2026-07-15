using ControleGastos.Api.Data;
using ControleGastos.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DE CORS (Autoriza o React a falar com a API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignora ciclos de referência infinita usando o caminho completo do System.Text.Json
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco e Serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ControleGastos.db"));

builder.Services.AddScoped<ControleGastos.Api.Services.IPessoaService, ControleGastos.Api.Services.PessoaService>(); 
builder.Services.AddScoped<ControleGastos.Api.Services.ITransacaoService, ControleGastos.Api.Services.TransacaoService>(); 
builder.Services.AddScoped<ControleGastos.Api.Services.IRelatorioService, ControleGastos.Api.Services.RelatorioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. ATIVAÇÃO DO CORS 
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();