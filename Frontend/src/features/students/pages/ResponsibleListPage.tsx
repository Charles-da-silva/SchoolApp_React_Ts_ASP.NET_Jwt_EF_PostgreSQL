import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { searchResponsibles } from "../services/StudentService";
import { ResponsibleCard } from "../components/ResponsibleCard";

export default function ResponsibleListPage() {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<any[]>([]);
  const navigate = useNavigate();

  async function load(q = "") {
    const res = await searchResponsibles(q);
    setResults(res);
  }

  useEffect(() => { void load(); }, []);

  async function handleSearch() {
    await load(query);
  }

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h1 className="text-3xl font-black text-slate-950">Responsáveis</h1>
        <div className="flex gap-2">
          <button onClick={() => void handleSearch()} className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700">Buscar</button>
          <button onClick={() => navigate('/responsibles/new')} className="rounded-lg bg-green-600 px-5 py-3 font-black text-white transition hover:bg-green-700">Adicionar Responsável</button>
        </div>
      </div>

      <div className="mt-4 flex gap-2">
        <input value={query} onChange={(e) => setQuery(e.target.value)} placeholder="Buscar por nome ou CPF" className="rounded border px-3 py-2 w-full" />
      </div>

      <div className="mt-5 grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {results.map((r) => (
          <ResponsibleCard key={r.id} responsible={r} />
        ))}
      </div>
    </div>
  );
}