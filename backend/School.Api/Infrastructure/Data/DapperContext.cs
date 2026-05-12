

using System.Data;
using Npgsql;


namespace School.Api.Infrastructure.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Cria a conexão com o banco
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("DefaultConnection")
        );
    }
}