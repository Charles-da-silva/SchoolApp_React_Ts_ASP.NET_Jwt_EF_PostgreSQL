

// Repository usado pelo Dapper

using Dapper;
using School.Api.Application.DTOs.StudentsAnamnesis;
using School.Api.Application.Interfaces.Repositories;
using School.Api.Infrastructure.Data;

namespace School.Api.Infrastructure.Repositories;

public class StudentAnamnesisReadRepository : IStudentAnamnesisReadRepository
{

    private readonly DapperContext _context;

    public StudentAnamnesisReadRepository (DapperContext context)
    {
        _context = context;
    }


    public async Task<StudentAnamnesisResponseDto?> GetByStudentIdAsync (Guid studentId)
    {
        var sql = @"
            SELECT
                a.id AS Id,
                a.student_id AS StudentId,
                a.content AS Content,
                a.created_at AS CreatedAt,
                a.updated_at AS UpdatedAt
            FROM student_anamneses a
            WHERE a.student_id = @Id
        ";

        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<StudentAnamnesisResponseDto>(
            sql,
            new { Id = studentId }
        );
    }

    
}