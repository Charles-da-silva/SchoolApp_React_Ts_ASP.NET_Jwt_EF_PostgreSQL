namespace School.Api.Application.Exceptions
{
    
    /// Exceção usada para representar erros de regra de negócio.
    /// 
    /// Por que criar isso?
    /// - Separar erro técnico de erro de regra de negócio
    /// - Permitir tratamento específico no futuro (middleware)
    /// - Melhorar legibilidade e semântica
    
    public class BusinessException : Exception
    {
        public object? Details { get; }

        public BusinessException(string message, object? details = null)
            : base(message)
        {
            Details = details;
        }
    }
}