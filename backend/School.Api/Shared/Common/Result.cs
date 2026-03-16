namespace School.Api.Application.Common
{
    /// Representa o resultado padronizado de uma operação.
    /// 
    /// Por que usar?
    /// - Evita usar Exception para fluxo esperado
    /// - Padroniza retorno da aplicação
    /// - Facilita tratamento no Controller
    /// - Reutilizável para qualquer entidade
   
    /* 
    Abaixo a explicação de porque temos dois Result:
        
        Result<T> → fica responsável por operações com dados:
            CreateStudent → retorna StudentResponseDto
            GetStudent → retorna StudentResponseDto

        Result → Mas existem operações que não precisam retornar dados:
            DeactivateStudent
            ReactivateStudent
            DeleteStudent
    
    */ 

    public class Result
    {
        public bool Success { get; protected set; }
        public string? Message { get; protected set; }
        public ErrorType ErrorType { get; protected set; }

        public static Result Ok(string message = "")
            => new Result { 
                Success = true, 
                Message = message 
                };

        public static Result Fail(string message, ErrorType errorType)
            => new Result { 
                Success = false, 
                Message = message, 
                ErrorType = errorType 
                };
    }

    public class Result<T> : Result
    {
        public T? Data { get; private set; }

        public static Result<T> Ok(T data)
            => new Result<T> { 
                Success = true, 
                Data = data 
                };

        public new static Result<T> Fail(string message, ErrorType errorType, T? data = default)
            => new Result<T> { 
                Success = false,
                Message = message,
                ErrorType = errorType,
                Data = data
                };
    }
    /*
    public class Result<T>
    {
        public bool Success { get; private set; }

        public string? Message { get; private set; }

        public ErrorType ErrorType { get; private set; }

        public T? Data { get; private set; }

        private Result() { }

        // Se OK, retorna true com os dados (response DTO)
        public static Result<T> Ok(T data, string? message = null)
{
            return new Result<T>
            {
                Success = true,
                Data = data,
                Message = message
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


    public class Result
    {
        public bool Success { get; private set; }

        public string? Message { get; private set; }

        public ErrorType ErrorType { get; private set; }

        private Result() { }

        // Operação bem-sucedida sem retorno de dados
        public static Result Ok(string? message = null)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }

        // Operação com falha
        public static Result Fail(string message, ErrorType errorType)
        {
            return new Result
            {
                Success = false,
                Message = message,
                ErrorType = errorType
            };
        }
    }
    */
}