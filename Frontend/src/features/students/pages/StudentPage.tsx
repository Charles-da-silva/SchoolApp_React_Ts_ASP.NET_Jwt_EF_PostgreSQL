import { useNavigate } from "react-router-dom";
import { StudentCard } from "../components/StudentCard";
import { useStudents } from "../hooks/useStudents";

export default function StudentsPage() {
  const { students, loading, error, reload } = useStudents();
  const navigate = useNavigate();

  if (loading) {
    return <p className="text-slate-600">Carregando alunos...</p>;
  }

  if (error) {
    return (
      <div>
        <p className="mb-4 text-red-700">{error}</p>
        <button className="rounded-lg bg-sky-600 px-4 py-2 font-bold text-white" onClick={reload}>
          Tentar novamente
        </button>
      </div>
    );
  }

  if (!students.length) {
    return <p className="text-slate-600">Nenhum aluno cadastrado.</p>;
  }

  return (
    <div>
      <h1 className="text-3xl font-black text-slate-950">Alunos</h1>

      <div className="my-5">
        <button
          className="rounded-lg bg-red-600 px-5 py-3 font-black text-white transition hover:bg-red-700"
          onClick={() => navigate("/students/new")}
        >
          Adicionar Aluno
        </button>
      </div>

      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {students.map((student) => (
          <StudentCard key={student.id} student={student} />
        ))}
      </div>
    </div>
  );
}
