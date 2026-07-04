import { api } from "../../../shared/services/api";
import type { Student } from "../types/Student";
import type { StudentDetailed } from "../types/StudentDetailed"
import type { StudentFilterData } from "../types/StudentFilterData";
import type { StudentFormData } from "../types/StudentFormData";

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
  const response = await api.get("/students/active");

  return response.data;  
};

export async function searchStudents(filters: StudentFilterData): Promise<Student[]> {
  const params = new URLSearchParams();

  if (filters.fullName?.trim()) {
    params.set("FullName", filters.fullName.trim());
  }

  if (filters.documentNumber?.trim()) {
    params.set("DocumentNumber", filters.documentNumber.trim());
  }

  if (filters.email?.trim()) {
    params.set("Email", filters.email.trim());
  }

  if (filters.isActive) {
    params.set("IsActive", filters.isActive);
  }

  if (filters.createdAfter) {
    params.set("CreatedAfter", filters.createdAfter);
  }

  if (filters.minAge) {
    params.set("MinAge", filters.minAge);
  }

  if (filters.maxAge) {
    params.set("MaxAge", filters.maxAge);
  }

  const response = await api.get(`/students?${params.toString()}`);
  return response.data;
}

export async function getStudentById(id: string) {
  const response = await api.get(`/students/${id}`)

  // seu backend retorna { data, success }
  return response.data.data as StudentDetailed
}

function toStudentPayload(data: StudentFormData) {
  const email = data.email?.trim();

  return {
    ...data,
    documentType: Number(data.documentType),
    email: email ? email : null,
  };
}

export async function createStudent(data: StudentFormData) {
  const response = await api.post("/students", toStudentPayload(data))
  return response.data
}

export async function updateStudent(id: string, data: StudentFormData) {
  const response = await api.put(`/students/${id}`, toStudentPayload(data))
  return response.data
}

export async function deactivateStudent(id: string) {
  await api.delete(`/students/${id}/deactivate`)
}

export async function reactivateStudent(id: string) {
  const response = await api.patch(`/students/${id}/reactivate`)
  return response.data
}

export async function deleteStudent(id: string) {
  await api.delete(`/students/${id}/delete`)
}
