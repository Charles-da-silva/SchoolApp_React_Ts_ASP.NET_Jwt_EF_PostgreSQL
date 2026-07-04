import { useState, type ChangeEvent, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { StudentCard } from "../components/StudentCard";
import { searchStudents } from "../services/StudentService";
import type { Student } from "../types/Student";
import type { StudentFilterData } from "../types/StudentFilterData";

const initialFilters: StudentFilterData = {
  fullName: "",
  documentNumber: "",
  email: "",
  isActive: "",
  createdAfter: "",
  minAge: "",
  maxAge: "",
};

export default function StudentSearchPage() {
  const navigate = useNavigate();
  const [filters, setFilters] = useState<StudentFilterData>(initialFilters);
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(false);
  const [hasSearched, setHasSearched] = useState(false);
  const [error, setError] = useState<string | null>(null);

  function handleChange(event: ChangeEvent<HTMLInputElement | HTMLSelectElement>) {
    setFilters((current) => ({
      ...current,
      [event.target.name]: event.target.value,
    }));
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    try {
      setLoading(true);
      setError(null);
      setHasSearched(true);

      const data = await searchStudents(filters);
      setStudents(data);
    } catch {
      setError("Nao foi possivel buscar alunos com estes filtros.");
      setStudents([]);
    } finally {
      setLoading(false);
    }
  }

  function handleClear() {
    setFilters(initialFilters);
    setStudents([]);
    setHasSearched(false);
    setError(null);
  }

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h1 className="text-3xl font-black text-slate-950">Buscar Alunos</h1>
        <button
          className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700"
          onClick={() => navigate("/students")}
        >
          Voltar
        </button>
      </div>

      <form onSubmit={handleSubmit} className="mt-5 rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
        <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
          <div>
            <label className="text-sm font-bold text-slate-700">Nome</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              name="fullName"
              value={filters.fullName}
              onChange={handleChange}
              placeholder="Parte do nome"
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">Número do documento</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              name="documentNumber"
              value={filters.documentNumber}
              onChange={handleChange}
              placeholder="CPF ou certidao"
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">E-mail</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              type="email"
              name="email"
              value={filters.email}
              onChange={handleChange}
              placeholder="Parte do e-mail"
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">Status</label>
            <select
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              name="isActive"
              value={filters.isActive}
              onChange={handleChange}
            >
              <option value="">Todos</option>
              <option value="true">Ativos</option>
              <option value="false">Inativos</option>
            </select>
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">Criado após</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              type="date"
              name="createdAfter"
              value={filters.createdAfter}
              onChange={handleChange}
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">Idade mínima</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              min="0"
              name="minAge"
              type="number"
              value={filters.minAge}
              onChange={handleChange}
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700">Idade máxima</label>
            <input
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              min="0"
              name="maxAge"
              type="number"
              value={filters.maxAge}
              onChange={handleChange}
            />
          </div>
        </div>

        <div className="mt-6 flex flex-wrap gap-2">
          <button
            className="rounded-lg bg-sky-600 px-5 py-3 font-black text-white transition hover:bg-sky-700 disabled:cursor-not-allowed disabled:bg-slate-300"
            disabled={loading}
          >
            {loading ? "Buscando..." : "Buscar"}
          </button>
          <button
            type="button"
            className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700"
            onClick={handleClear}
          >
            Limpar
          </button>
        </div>
      </form>

      {error ? <p className="mt-5 text-red-700">{error}</p> : null}

      {hasSearched && !loading && !error ? (
        <div className="mt-6">
          <h2 className="text-xl font-black text-slate-950">Resultados</h2>
          {students.length ? (
            <div className="mt-4 grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
              {students.map((student) => (
                <StudentCard key={student.id} student={student} />
              ))}
            </div>
          ) : (
            <p className="mt-4 text-slate-600">Nenhum aluno encontrado.</p>
          )}
        </div>
      ) : null}
    </div>
  );
}
