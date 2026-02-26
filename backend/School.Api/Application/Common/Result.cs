namespace School.Api.Application.Common
{
    /// <summary>
    /// Representa o resultado padronizado de uma operação.
    /// 
    /// Por que usar?
    /// - Evita usar Exception para fluxo esperado
    /// - Padroniza retorno da aplicação
    /// - Facilita tratamento no Controller
    /// - Reutilizável para qualquer entidade
    /// </summary>
    public class Result<T>
    {
        public bool Success { get; private set; }

        public string? Message { get; private set; }

        public ErrorType ErrorType { get; private set; }

        public T? Data { get; private set; }

        private Result() { }

        // Se OK, retorna true com os dados (response DTO)
        public static Result<T> Ok(T data)
        {
            return new Result<T>
            {
                Success = true,
                Data = data
            };
        }

        // Se falha, retorna false, mensagem de erro, tipo de erro e dados (response DTO)
        public static Result<T> Fail(string message, ErrorType errorType, T? data = default)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                ErrorType = errorType,
                Data = data
            };
        }
    }
}