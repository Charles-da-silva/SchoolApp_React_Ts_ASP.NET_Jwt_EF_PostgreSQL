
/*  ================================
    ESTE ARQUIVO É O CORAÇÃO DA APLICAÇÃO
    ===============================*/
    
// Namespace do Entity Framework Core
using Microsoft.EntityFrameworkCore;

// Nosso DbContext
using School.Api.Infrastructure.Data;
using School.Api.Application.Interfaces.Services;
using School.Api.Application.Services;
using School.Api.Application.Interfaces.Repositories;
using School.Api.Infrastructure.Repositories;

// Cria o "builder" da aplicação.
// Ele é responsável por:
// - Ler configurações
// - Registrar serviços (Dependency Injection)
// - Preparar o pipeline HTTP
var builder = WebApplication.CreateBuilder(args);

/// ===============================
/// REGISTRO DE SERVIÇOS (Injeção de Dependência)
/// ===============================

// Usamos o builder.Services para registrar serviços na aplicação,
// registrando tudo o que a aplicação usa: Controllers, Banco, Autenticação, Services, Cache

// Adiciona suporte a Controllers (MVC Web API).
// Sem isso, o ASP.NET NÃO reconhece classes Controller.
builder.Services.AddControllers();

/*  Isso diz ao .NET: Quando alguém pedir IStudentService, entregue StudentService.
    Isso é conhecido como:
        - Injeção de Dependência (DI) - o Controller depende da abstração (interface) e não da implementação concreta.
        - Inversão de Controle (IoC) - a classe não cria suas dependências, elas são fornecidas pelo framework.
        - Isso segue o Dependency Inversion Principle (SOLID) - dependemos de abstrações,
*/
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentAnamnesisService, StudentAnamnesisService>();

builder.Services.AddScoped<IStudentAnamnesisRepository, StudentAnamnesisRepository>();
builder.Services.AddScoped<IStudentAnamnesisReadRepository, StudentAnamnesisReadRepository>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentReadRepository, StudentReadRepository>();

// Registra serviços necessários para o Swagger/OpenAPI.
// Isso permite documentar e testar a API via navegador.
builder.Services.AddEndpointsApiExplorer();

// Swagger (Swashbuckle) - documentação da API
builder.Services.AddSwaggerGen();

// =======================
// CONFIGURAÇÃO DO EF CORE
// =======================
//
// Aqui estamos dizendo:
// - Use o SchoolDbContext
// - Use PostgreSQL como banco
// - Use a connection string chamada "DefaultConnection"
//
builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
    .UseSnakeCaseNamingConvention();

    /* 
    UseNpgsql -> Configura o EF Core para usar PostgreSQL
    GetConnectionString -> Lê a connection string do appsettings.json
    UseSnakeCaseNamingConvention -> Converte automaticamente:
        Students -> students
        FullName -> full_name
        DateOfBirth -> date_of_birth

        Isso porque o padrão em C# é PascalCase mas no Postgres
        o padrão é snake_case, então é preciso fazer essa conversão.
    */
});

// Configuração do Dapper
builder.Services.AddScoped<DapperContext>();


// Configuração de CORS (Cross-Origin Resource Sharing)
// Isso é necessário para permitir que o frontend (que roda em outro domínio/porta) acesse a API 
// sem bloqueios de segurança do navegador.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

/// ===============================
/// BUILD DA APLICAÇÃO
/// ===============================

// Aqui a aplicação é efetivamente construída.
// Depois disso, não é mais possível registrar serviços.
var app = builder.Build();

/// ===============================
/// CONFIGURAÇÃO DO PIPELINE HTTP (middlewares)
/// ===============================

// Em ambiente de desenvolvimento:
// - habilitamos Swagger
// - habilitamos a UI do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o JSON do Swagger

    // Interface gráfica do Swagger
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "School API v1");
    });
}

// Força redirecionamento HTTP → HTTPS
// Boa prática de segurança.
app.UseHttpsRedirection();

// Habilita CORS para a política "AllowFrontend" que definimos acima.
// Deve ser chamado antes de MapControllers para que as regras de CORS sejam aplicadas 
// às rotas dos Controllers.
app.UseCors("AllowFrontend");

// Autorização (vamos usar quando entrar JWT)
app.UseAuthorization();

/* Mapeia os Controllers.
    Isso faz com que:
        - [ApiController]
        - [HttpGet], [HttpPost], etc.
    comecem a funcionar.
    Sem isso, o ASP.NET não reconhece os Controllers e as rotas.
*/
app.MapControllers();

/*  ================================
    START DA APLICAÇÃO
    ===============================*/

//Inicia o servidor web / Sobe a aplicação 
app.Run();

