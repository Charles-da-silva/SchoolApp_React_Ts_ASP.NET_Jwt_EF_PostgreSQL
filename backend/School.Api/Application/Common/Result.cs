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

        public T? Data { get; private set; }

        public bool CanReactivate { get; private set; }

        private Result() { }

        public static Result<T> Ok(T data)
        {
            return new Result<T>
            {
                Success = true,
                Data = data
            };
        }

        public static Result<T> Fail(string message, T? data = default, bool canReactivate = false)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Data = data,
                CanReactivate = canReactivate
            };
        }
    }
}