
// Este arquivo espelha a estrutura do modelo de dados do Student no backend, para que possamos usar
// os mesmos tipos no frontend. Mais precisamente, ele reflete o StudentDto que criamos no backend.

export interface Student {
  id: string;
  fullName: string;
  email: string;
  dateOfBirth: string;
  isActive: boolean;
}