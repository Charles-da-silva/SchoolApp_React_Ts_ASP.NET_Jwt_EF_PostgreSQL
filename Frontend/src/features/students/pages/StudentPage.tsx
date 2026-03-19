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
    // Enquanto API responde → mostra feedback.
  }

  if (error) {
    return (
      <div>
        <p>{error}</p>

        {/* reload vem do hook, que na verdade recebe a função 
        loadStudents() do próprio hook. Assim Se backend falhar
        o usuário pode tentar novamente. */}
        <button onClick={reload}>Tentar novamente</button>
      </div>
    
    );
  }

  if (!students.length) {  {/*API funcionou mas não há dados.*/}
    return <p>Nenhum aluno cadastrado.</p>;  
  }

  // Success state. Só aqui renderizamos a página com o conteúdo.
  return (

    <div style={{ padding: "20px" }}>
      <h1>Alunos</h1>

      <div style={{ marginBottom: 20 }}>
        
        {/* Navegando para a rota /students/new declarada no arquivo router.tsx
            React Router muda URL sem reload.*/}
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