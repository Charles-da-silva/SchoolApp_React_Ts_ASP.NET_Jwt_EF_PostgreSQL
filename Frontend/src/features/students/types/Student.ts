
// Este arquivo espelha a estrutura do modelo de dados do Student no 
// backend, para que possamos usar os mesmos tipos no frontend. 
// Mais precisamente, ele reflete o StudentDto que criamos no backend.

export type Student = {
  id: string;
  fullName: string;
  documentType: string;
  documentNumber: string;
  dateOfBirth: string;
  isActive: boolean;
};