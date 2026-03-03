import axios from "axios";
import type { Student } from "../types/Student";

const API_URL = "http://localhost:5101/api/students";

export const getStudents = async (): Promise<Student[]> => {
  const response = await axios.get(API_URL);

  console.log("Resposta completa:", response.data);

  return response.data;
};
  // porque seu backend retorna Result<T>
  // então vem { success, message, errorType, data }