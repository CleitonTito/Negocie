using Negocie._3_Integracoes;
using Negocie._3_Integracoes.Interfaces;
using Negocie._3_Integracoes.Refit;
using Negocie._3_Integracoes.Repositories;
using Refit;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();

builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://viacep.com.br");
});

const string connectionString = "Host=no-db-dev-101.negocieonline.com.br; Database = db_selecao_imdb; User Id = usr_teste; Password = Teste@2022;";
builder.Services.AddScoped<IDbConnection>((sp) => DbConnection.CreateConnection(connectionString));
builder.Services.AddScoped<ICepRepository, CepRepository>();

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
