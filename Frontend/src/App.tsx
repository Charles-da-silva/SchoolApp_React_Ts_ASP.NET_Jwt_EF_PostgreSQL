import { useEffect, useState } from "react";
import type { Student } from "./types/Student";
import { getStudents } from "./services/studentServices";

function App() {
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const data = await getStudents();
        console.log("Data no App:", data);
        setStudents(data);
      } catch (error) {
        console.error("Erro ao buscar alunos:", error);
      } finally {
        setLoading(false);
      }
    };

    

    fetchStudents();
  }, []);

  /* A função abaixo foi criada porque mesmo o backend enviando a data no formato correto, o frontend 
    estava exibindo a data de forma errada (1 dia a menos), quando usando a função new Date(), então
    essa função foi criada para tratar a data como string e não ter problemas com fuso horário. */
  const formatDate = (date: string) => {
      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
  };

  return (
    <div style={{ padding: 40 }}>
      <h1>Lista de Alunos</h1>

      {loading && <p>Carregando...</p>}

      {!loading && students.length === 0 && (
        <p>Nenhum aluno encontrado.</p>
      )}

      {!loading &&
        students.map((student) => (
          <div key={student.id}>
            <strong>{student.fullName}</strong>
            <div>{student.email}</div>
            <div>Status: {student.isActive ? "Ativo" : "Inativo"}</div>
            <div>Data de Nascimento: {formatDate(student.dateOfBirth)}</div>
            <hr />
          </div>
        ))}
    </div>
  );
}

export default App;