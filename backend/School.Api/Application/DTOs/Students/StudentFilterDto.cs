
// Filter que será usado para filtrar os alunos na consulta GET api/students
public class StudentFilterDto
{
    public bool? IsActive { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAfter { get; set; }
}