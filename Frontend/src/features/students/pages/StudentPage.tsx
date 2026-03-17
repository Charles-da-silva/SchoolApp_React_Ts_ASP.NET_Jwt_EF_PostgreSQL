/*
Page = orquestradora.
Ela que coordena como os StudendCards serão exibidos

Ela conecta:
Hook → Components
*/

import { useStudents } from "../hooks/useStudents";
import { StudentCard } from "../components/StudentCard";
import { useNavigate } from "react-router-dom";

export default function StudentsPage() {
  
  //returns do Hook useStudents.ts
  const { students, loading, error, reload } = useStudents();

  const navigate = useNavigate();

  if (loading) {
    return <p>Carregando alunos...</p>;
  }

  if (error) {
    return (
      <div>
        <p>{error}</p>

        {/* reload vem do hook */}
        <button onClick={reload}>Tentar novamente</button>
      </div>
    
    );
  }

  if (!students.length) {
    return <p>Nenhum aluno cadastrado.</p>;
  }

  // success state
  return (
    <div style={{ padding: "20px" }}>
      <h1>Alunos</h1>

      <div style={{ marginBottom: 20 }}>
        {/* navegando para a rota /students/new declarada no arquivo router.tsx*/}
        <button onClick={() => navigate("/students/new")}>
          Adicionar Aluno
        </button>
      </div>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fill, minmax(280px, 1fr))",
          gap: "16px",
        }}
      >
        {students.map((student) => (
          <StudentCard key={student.id} student={student} />
        ))}
      </div>
    </div>
  );
}