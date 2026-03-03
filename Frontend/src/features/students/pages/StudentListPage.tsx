// Importa hooks do React
// useState → para armazenar estado local
// useEffect → para executar efeito colateral (buscar dados ao montar a página)
import { useEffect, useState } from "react";

// Importa o tipo Student (contrato de dados vindo do backend)
import type { Student } from "../types/Student";

import { StudentCard } from "../../../shared/components/StudentCard";

// Importa o serviço responsável por comunicação com API
// OBS: Página não deve saber detalhes de axios — isso é responsabilidade do service
import { getStudents } from "../services/studentService";

export function StudentListPage() {
  // Estado que armazena a lista de alunos
  const [students, setStudents] = useState<Student[]>([]);

  // Estado de loading para melhorar UX
  const [loading, setLoading] = useState<boolean>(true);

  // useEffect executa quando o componente é montado
  // O segundo parâmetro [] significa: execute apenas uma vez
  useEffect(() => {
    async function fetchStudents() {
      try {
        // Chamada para API através da camada de service
        const data = await getStudents();

        // Atualiza o estado com os dados recebidos
        setStudents(data);
      } catch (error) {
        console.error("Erro ao buscar estudantes:", error);
      } finally {
        // Independentemente de sucesso ou erro, removemos o loading
        setLoading(false);
      }
    }

    fetchStudents();
  }, []);

  /* A função abaixo foi criada porque mesmo o backend enviando a data no formato correto, o frontend 
    estava exibindo a data de forma errada (1 dia a menos), quando usando a função new Date(), então
    essa função foi criada para tratar a data como string e não ter problemas com fuso horário. */
  const formatDate = (date: string) => {
      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
  };

  // Tratamento de loading
  if (loading) {
    return <p>Carregando alunos...</p>;
  }

  // Tratamento de lista vazia
  if (students.length === 0) {
    return <p>Nenhum aluno encontrado.</p>;
  }

  // Renderização principal
  return (
    <div style={{ padding: 40 }}>
      <h1>Lista de Alunos</h1>

      {/* Mapeia a lista de alunos e renderiza um StudentCard para cada um */}
      {students.map((student) => (
        <StudentCard key={student.id} student={student} />
      ))}
    </div>
  );
}