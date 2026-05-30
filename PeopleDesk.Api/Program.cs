using Microsoft.OpenApi;
using PeopleDesk.Api.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PeopleDeskEnterprise API",
        Version = "v1",
        Description = "API responsável pelos recursos do sistema PeopleDeskEnterprise, incluindo operações de cadastro, consulta, atualização e gerenciamento de chamados.",
        TermsOfService = new Uri("https://www.sistemapeoplenet.com.br/termos"),
        Contact = new OpenApiContact
        {
            Name = "Equipe PeopleDesk",
            Email = "suporte@cotiinformatica.com.br",
            Url = new Uri("https://www.cotiinformatica.com.br")
        },
        License = new OpenApiLicense
        {
            Name = "Uso interno - PeopleDesk",
            Url = new Uri("https://www.cotiinformatica.com.br")
        }
    });
});

// Injeção de dependências (IoC)
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PeopleDeskEnterprise API v1");
    options.DocumentTitle = "PeopleDeskEnterprise API - Swagger";
    options.RoutePrefix = "swagger";
});

app.UseAuthorization();

app.MapControllers();

app.Run();