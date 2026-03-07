// Importa hooks do React
// useState → para armazenar estado local
// useEffect → para executar efeito colateral (buscar dados ao montar a página)
import { useEffect, useState } from "react";

// Importa o tipo Student (contrato de dados vindo do backend)
import type { Student } from "../types/Student";

import { StudentCard } from "../components/StudentCard";

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

        // Console de controle para verificar o que está chegando da API
        console.log("Tipo de data:", typeof data);
        console.log("Data:", data);
        console.log("É array?", Array.isArray(data));

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

  // Tratamento de loading
  if (loading) {
    return <p>Carregando alunos...</p>;
  }

  // Tratamento de lista vazia
  if (students.length === 0) {
    return <p>Nenhum aluno encontrado.</p>;
  }

  // Função responsável por lidar com a desativação
  // Ela pertence à Page porque é comportamento da tela
  async function handleDeactivate(id: string) {
    try {
      console.log("Desativando aluno com id:", id);

      // Aqui futuramente chamaremos o backend
      // await deactivateStudent(id);

      // Atualizamos o estado local para refletir a mudança
      setStudents((prevStudents) =>
        prevStudents.map((student) =>
          student.id === id
            ? { ...student, isActive: false }
            : student
        )
      );

    } catch (error) {
      console.error("Erro ao desativar aluno:", error);
    }
  }

  // Renderização principal
  return (
    <div style={{ padding: 40 }}>
      <h1>Lista de Alunos</h1>

      {/* Mapeia a lista de alunos e renderiza um StudentCard para cada aluno */}
      {students.map((student) => (
        <StudentCard
          key={student.id}
          student={student}
          onDeactivate={handleDeactivate}
        />
      ))}
    </div>
  );
}