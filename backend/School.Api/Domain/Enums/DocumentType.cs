namespace School.Api.Domain.Enums
{
    /*
        Enum representa os tipos possíveis de documento do aluno.

        Vantagens:
        - Evita strings inválidas
        - Facilita validação
        - Melhora legibilidade
        - EF Core salva automaticamente como int (por padrão)
    */
    public enum DocumentType
    {
        CPF = 1,
        
        BirthCertificateNumber = 2
    }
}