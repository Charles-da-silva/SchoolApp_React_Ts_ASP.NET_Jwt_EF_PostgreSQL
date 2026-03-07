import axios from "axios";
import type { Student } from "../types/Student";

/*
URL base da API.

Em projetos maiores isso vai para:
"config/api.ts" ou ".env"
*/
const API_URL = "http://localhost:5101/api/students";

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

  // Fazendo a requisição HTTP...
  const response = await axios.get<Student[]>(API_URL);

  // Console de controle para verificar o que está chegando da API
  console.log("Resposta completa:", response);
  console.log("response.data:", response.data);
  console.log("response.data.data:", response.data);

  return response.data;

  
};