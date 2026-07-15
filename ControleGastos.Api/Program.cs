using ControleGastos.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ControleGastos.db"));

// Injeção de dependência usando o caminho completo (sem o using de Services no topo)
builder.Services.AddScoped<ControleGastos.Api.Services.IPessoaService, ControleGastos.Api.Services.PessoaService>();
builder.Services.AddScoped<ControleGastos.Api.Services.ITransacaoService, ControleGastos.Api.Services.TransacaoService>();
builder.Services.AddScoped<ControleGastos.Api.Services.IRelatorioService, ControleGastos.Api.Services.RelatorioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();