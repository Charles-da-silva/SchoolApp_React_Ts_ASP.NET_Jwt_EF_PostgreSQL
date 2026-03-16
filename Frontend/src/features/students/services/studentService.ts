import { api } from "../../../shared/services/api";
import type { Student } from "../types/Student";

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
export async function getStudents(): Promise<Student[]> {

  // Fazendo a requisição HTTP...
  const response = await api.get("/students");

  return response.data;

  
};