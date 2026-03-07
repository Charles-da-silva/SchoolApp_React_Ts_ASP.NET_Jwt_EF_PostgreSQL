import axios from "axios";
import type { Student } from "../types/Student";

/*
URL base da API.

Em projetos maiores isso vai para:
"config/api.ts" ou ".env"
*/
const API_URL = "http://localhost:5101/api/students";

/*
Representa o padrão de resposta do backend.

Seu backend retorna um wrapper:   Result<T>
{
  success: boolean
  message: string | null
  errorType: string | null
  data: T
}
*/
type ApiResponse<T> = {
  success: boolean;
  message: string | null;
  errorType: string | null;
  data: T;
};

/*
Função responsável por buscar estudantes no backend.

IMPORTANTE:
Pages e Components NÃO devem usar axios diretamente.
Toda comunicação HTTP deve passar pela camada de service.
*/

/* export - Significa que essa função pode ser importada por outros arquivos
com:   import { getStudents } from "../services/studentService";
Sem export, a função fica privada ao arquivo.

(): Promise<Student[]>  Essa é a tipagem do retorno da função.
Significa que essa função retorna uma Promise que no final terá um array
de Students. Quando usamos async, a função NÃO retorna o valor diretamente.
Ela retorna uma Promise. 
Uma Promise significa:"Eu vou te entregar esse valor no futuro"
*/
export const getStudents = async (): Promise<Student[]> => {

  /* Fazendo a requisição HTTP...
  O axios sempre retorna um objeto assim:
  { data: (conteúdo da API),
    status: 200,
    statusText: "OK",
    headers: {...}
  }
    dentro do data é que está o conteúdo que a API enviou. No nosso caso, 
    é o ApiResponse<Student[]> */
  const response = await axios.get<ApiResponse<Student[]>>(API_URL);

  console.log("Resposta completa da API:", response.data);

  /* Aqui extraímos apenas o que a aplicação precisa:  a lista de alunos.
  Fica response.data.data porque response.data é o objeto de resposta completo:
  {  success
     message
      errorType
      data  
  }
  Por isso fica response.data.data para acessar a propriedade data dentro 
  do objeto de resposta.

  Ou seja:

  A função remove o envelope da API e entrega apenas o dado útil.
  Isso é boa prática de arquitetura.
  O React não precisa saber que existe:
    success
    message
    errorType
  */
  return response.data.data;

  
};