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
    return (
      <div>
        <div className="flex flex-wrap items-center justify-between gap-3">
          <h1 className="text-3xl font-black text-slate-950">Alunos</h1>
          <div className="flex flex-wrap gap-2">
            <button
              className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700"
              onClick={reload}
            >
              Recarregar
            </button>
            <button
              className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700"
              onClick={() => navigate("/students/search")}
            >
              Buscar
            </button>
            <button
              className="rounded-lg bg-green-600 px-5 py-3 font-black text-white transition hover:bg-green-700"
              onClick={() => navigate("/students/new")}
            >
              Adicionar Aluno
            </button>
          </div>
        </div>
        <p className="mt-5 text-slate-600">Nenhum aluno cadastrado.</p>
      </div>
    );
  }

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h1 className="text-3xl font-black text-slate-950">Alunos</h1>
        <div className="flex flex-wrap gap-2">
          
          <button
            className="rounded-lg bg-yellow-300 px-5 py-3 font-black text-gray transition hover:bg-yellow-500"
            onClick={() => navigate("/students/search")}
          >
            Buscar Alunos
          </button>
        <button
          className="rounded-lg bg-green-600 px-5 py-3 font-black text-white transition hover:bg-green-700"
          onClick={() => navigate("/students/new")}
        >
          Adicionar Aluno
        </button>
        </div>
      </div>

      <div className="mt-5 grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {students.map((student) => (
          <StudentCard
            key={student.id}
            student={student}
          />
        ))}
      </div>
    </div>
  );
}
