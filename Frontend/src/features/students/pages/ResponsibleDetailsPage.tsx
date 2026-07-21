import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getResponsibleById, getStudentResponsiblesByResponsible, searchStudents, createStudentResponsible } from "../services/StudentService";

export default function ResponsibleDetailsPage() {
  const { id } = useParams();
  const [responsible, setResponsible] = useState<any | null>(null);
  const [links, setLinks] = useState<any[]>([]);
  const [query, setQuery] = useState("");
  const [students, setStudents] = useState<any[]>([]);

  async function load() {
    if (!id) return;
    const r = await getResponsibleById(id);
    setResponsible(r);
    const l = await getStudentResponsiblesByResponsible(id);
    setLinks(l);
  }

  useEffect(() => { void load(); }, [id]);

  async function handleSearchStudents() {
    const res = await searchStudents({ fullName: query });
    setStudents(res);
  }

  async function handleLinkStudent(studentId: string) {
    if (!id) return;
    const payload = {
      studentId,
      responsibleId: id,
      relationshipType: "Guardian",
      canPickUpChild: false,
      isFinanceContact: false,
      isLegalResponsable: false,
      observation: ""
    };
    await createStudentResponsible(payload);
    await load();
  }

  if (!responsible) return <div className="p-6">Carregando...</div>;

  return (
    <div className="p-6">
      <h1 className="text-2xl font-black">{responsible.fullName}</h1>
      <p className="text-slate-600">CPF: {responsible.cpf}</p>

      <hr className="my-4" />

      <h3 className="font-black">Estudantes Vinculados</h3>
      <div className="mt-3">
        {links.map((l) => (
          <div key={l.id} className="border p-3 mb-2">
            <div className="font-bold">{l.student?.fullName}</div>
            <div className="text-sm text-slate-600">{l.relationshipType}</div>
          </div>
        ))}
      </div>

      <hr className="my-4" />

      <h4 className="font-black">Vincular novo estudante</h4>
      <div className="mt-2 flex gap-2">
        <input value={query} onChange={(e) => setQuery(e.target.value)} placeholder="Buscar por nome" className="rounded border px-3 py-2" />
        <button onClick={() => void handleSearchStudents()} className="rounded bg-sky-600 px-3 py-2 text-white">Buscar</button>
      </div>

      <div className="mt-3">
        {students.map(s => (
          <div key={s.id} className="flex items-center justify-between border p-3">
            <div>
              <div className="font-bold">{s.fullName}</div>
              <div className="text-sm text-slate-600">{s.documentNumber}</div>
            </div>
            <div>
              <button onClick={() => void handleLinkStudent(s.id)} className="rounded bg-green-600 px-3 py-2 text-white">Vincular</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}