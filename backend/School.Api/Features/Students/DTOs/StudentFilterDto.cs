
// Filter que será usado para filtrar os alunos na consulta GET api/students
public class StudentFilterDto
{
    public bool? IsActive { get; set; }
    public string? Name { get; set; }
    public string? DocumentNumber { get; set; }
    public DateOnly? CreatedAfter { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
}