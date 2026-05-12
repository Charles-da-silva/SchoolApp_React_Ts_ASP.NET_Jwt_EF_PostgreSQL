

// esse repository será usado pelo Dapper e não pelo EF Core

using System.Text;
using Dapper;
using School.Api.Application.DTOs.Students;
using School.Api.Application.Interfaces.Repositories;
using School.Api.Infrastructure.Data;

namespace School.Api.Infrastructure.Repositories;

public class StudentReadRepository : IStudentReadRepository
{
    private readonly DapperContext _context;

    
    public StudentReadRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync(StudentFilterDto filter)
    {
        // StringBuilder ajuda a montar SQL dinâmico, para depois usar o sql.Append
        /* aqui em baixo estamos fazendo um mapping (usando AS) para o Dapper
        conseguir mapear a coluna na tabela com a propriedade no C# */
        var sql = new StringBuilder(@"
            SELECT
                id AS Id,
                full_name AS FullName,
                email AS Email,
                document_type AS DocumentType,
                document_number AS DocumentNumber,
                is_active AS IsActive
            FROM students
            WHERE 1 = 1"
        );

        // Parameters evita SQL Injection
        var parameters = new DynamicParameters();

        // Filtro por nome
        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            sql.Append(" AND full_name ILIKE @FullName");
            parameters.Add("FullName", $"%{filter.FullName}%");
        }

        // Filtro por status
        if (filter.IsActive.HasValue)
        {
            sql.Append(" AND is_active = @IsActive");
            parameters.Add("IsActive", filter.IsActive.Value);
        }

        // Filtro por número do documento
        if (!string.IsNullOrWhiteSpace(filter.DocumentNumber))
        {
            sql.Append(" AND document_number ILIKE @DocumentNumber");

            parameters.Add(
                "DocumentNumber",
                $"%{filter.DocumentNumber}%"
            );
        }

        // Filtros por idade
        if (filter.MinAge.HasValue)
        {
            var maxBirthDate =
                DateOnly.FromDateTime(
                    DateTime.UtcNow.AddYears(-filter.MinAge.Value)

                /* Aqui estamos calculando a idade mínima, por exemplo, 5 anos.
                Estamos trabalhando com datas, pois o banco tem a data de nascimento, não a idade.
                Então, para filtrar por idade, precisamos converter a idade em uma data de nascimento 
                máxima, qual está sendo calculada com DateTime.UtcNow.AddYears(-filter.MinAge.Value).
                Se quisermos filtrar por alunos com idade mínima de 5 anos, a data de nascimento deve 
                ser menor ou igual a 5 anos atrás, ou seja, DataNascimento <= hoje - 5 anos.

                A observação é que estamos usando o DateOnly, pois DateTime traz horas além da data
                e na conversão para pt-br no Front (UTC para horário local) dependendo do horário do 
                registro de nascimento, pode ser exibido um dia a menos*/
                );

            sql.Append(" AND date_of_birth <= @MaxBirthDate");

            parameters.Add(
                "MaxBirthDate", maxBirthDate.ToDateTime(TimeOnly.MinValue));
            // fazendo Add com ToDateTime porque o Dapper não trabalha com DateOnly
        }

        if (filter.MaxAge.HasValue)
        {
            var minBirthDate =
                DateOnly.FromDateTime(
                    DateTime.UtcNow.AddYears(-filter.MaxAge.Value)
                
                /* Aqui estamos calculando a idade máxima, por exemplo, 10 anos.
                A lógica é semelhante ao filtro de idade mínima, mas invertida. Para filtrar por alunos
                com idade máxima de 10 anos, a data de nascimento deve ser maior ou igual a 10 anos atrás, 
                ou seja, DataNascimento >= hoje - 10 anos.
                */
                );

            sql.Append(" AND date_of_birth >= @MinBirthDate");

            parameters.Add(
                "MinBirthDate", minBirthDate.ToDateTime(TimeOnly.MinValue));
        }


        // Ordenação
        sql.Append(" ORDER BY full_name ASC");

        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<StudentResponseDto>(
            sql.ToString(),
            parameters
        );
    }

    public async Task<StudentResponseDetailedDto?> GetDetailedByIdAsync(Guid id)
    {
        var sql = @"
            SELECT
                s.id AS Id,
                s.full_name AS FullName,
                s.date_of_birth AS DateOfBirth,
                s.document_type AS DocumentType,
                s.document_number AS DocumentNumber,
                s.email AS Email,
                s.is_active AS IsActive,
                s.created_at AS CreatedAt,
                s.deactivated_at AS DeactivatedAt
            FROM students s
            WHERE s.id = @Id
        ";
        
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<StudentResponseDetailedDto>(
            sql,
            new { Id = id }
        );
    }
}