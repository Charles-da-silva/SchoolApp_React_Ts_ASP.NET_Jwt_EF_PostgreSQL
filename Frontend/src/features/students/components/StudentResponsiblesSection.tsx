import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import {
  getStudentResponsibles,
  searchResponsibles,
  createStudentResponsible,
  deleteStudentResponsible,
  createResponsible,
} from "../services/StudentService";

interface Props {
  studentId: string;
}

export default function StudentResponsiblesSection({ studentId }: Props) {
  const [links, setLinks] = useState<any[]>([]);
  const [query, setQuery] = useState("");
  const [suggestions, setSuggestions] = useState<any[]>([]);
  const [selected, setSelected] = useState<any | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({
    relationshipType: "Father",
    canPickUpChild: false,
    isFinanceContact: false,
    isLegalResponsable: false,
    observation: "",
  });

  const relationshipLabels: Record<string, string> = {
    Father: "Pai",
    Mother: "Mãe",
    Grandfather: "Avô",
    Grandmother: "Avó",
    Brother: "Irmão",
    Sister: "Irmã",
    Guardian: "Responsável",
    Other: "Outro",
  };

  async function load() {
    const data = await getStudentResponsibles(studentId);
    setLinks(data);
  }

  useEffect(() => { void load(); }, [studentId]);

  useEffect(() => {
    const t = setTimeout(async () => {
      if (!query || query.length < 2) {
        setSuggestions([]);
        return;
      }
      const res = await searchResponsibles(query);
      setSuggestions(res);
    }, 300);
    return () => clearTimeout(t);
  }, [query]);

  function openFor(responsible: any) {
    setSelected(responsible);
    setForm({ relationshipType: "Father", canPickUpChild: false, isFinanceContact: false, isLegalResponsable: false, observation: "" });
    setShowModal(true);
  }

  async function handleCreateRelation() {
    if (!selected) return;
    try {
      await createStudentResponsible({
        studentId,
        responsibleId: selected.id,
        relationshipType: form.relationshipType,
        canPickUpChild: form.canPickUpChild,
        isFinanceContact: form.isFinanceContact,
        isLegalResponsable: form.isLegalResponsable,
        observation: form.observation,
      });

      toast.success("Vínculo criado com sucesso");
      setShowModal(false);
      setSelected(null);
      await load();
    } catch (err: any) {
      console.error(err);
      const message = err?.response?.data || err?.message || "Erro ao criar vínculo";
      toast.error(String(message));
    }
  }

  async function handleUnlink(id: string) {
    if (!confirm("Deseja realmente desvincular?")) return;
    await deleteStudentResponsible(id);
    await load();
  }

  const [showCreateResponsible, setShowCreateResponsible] = useState(false);
  const [responsibleForm, setResponsibleForm] = useState({
    fullName: "",
    cpf: "",
    email: "",
    phone: "",
    profession: "",
    address: "",
  });

  async function handleCreateResponsibleAndLink() {
    // basic frontend validation
    if (!responsibleForm.fullName || responsibleForm.fullName.trim().length < 3) {
      alert('Informe o nome completo do responsável (mínimo 3 caracteres)');
      return;
    }

    const payload = {
      fullName: responsibleForm.fullName,
      cpf: responsibleForm.cpf || null,
      email: responsibleForm.email || null,
      phone: responsibleForm.phone || null,
      profession: responsibleForm.profession || null,
      address: responsibleForm.address || null,
    };

    const created = await createResponsible(payload);
    setShowCreateResponsible(false);
    setResponsibleForm({ fullName: "", cpf: "", email: "", phone: "", profession: "", address: "" });
    openFor(created);
  }

  return (
    <div className="mt-6 rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
      <h3 className="text-xl font-black">Responsáveis</h3>

      <div className="mt-3">
        <input
          placeholder="Buscar por nome ou CPF"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          className="w-full rounded border px-3 py-2"
        />
        <div className="mt-2">
          {suggestions.map((s) => (
            <div key={s.id} className="flex items-center justify-between border-b py-2">
              <div>
                <div className="font-bold">{s.fullName}</div>
                <div className="text-sm text-slate-600">{s.cpf || s.email || ""}</div>
              </div>
              <div>
                <button onClick={() => openFor(s)} className="rounded bg-green-600 px-3 py-1 text-white">Vincular</button>
              </div>
            </div>
          ))}
        </div>

        <div className="mt-3">
          <button onClick={() => setShowCreateResponsible(true)} className="rounded bg-sky-600 px-3 py-2 text-white">Cadastrar novo responsável</button>
        </div>
      </div>

      <hr className="my-4" />

      <div>
        {links.length === 0 ? (
          <div className="text-slate-600">Nenhum responsável vinculado.</div>
        ) : (
          <ul className="space-y-2">
            {links.map((l) => (
              <li key={l.id} className="flex items-center justify-between border p-3">
                <div>
                  <div className="font-bold">{l.responsible?.fullName || l.responsible?.fullName}</div>
                  <div className="text-sm text-slate-600">{relationshipLabels[l.relationshipType] ?? l.relationshipType}</div>
                </div>
                <div className="flex gap-2">
                  <button onClick={() => { setSelected(l.responsible); setForm({ relationshipType: l.relationshipType, canPickUpChild: l.canPickUpChild, isFinanceContact: l.isFinanceContact, isLegalResponsable: l.isLegalResponsable, observation: l.observation || "" }); setShowModal(true); }} className="rounded bg-amber-500 px-3 py-1 text-white">Editar vínculo</button>
                  <button onClick={() => void handleUnlink(l.id)} className="rounded bg-red-500 px-3 py-1 text-white">Desvincular</button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>

      {/* modal */}
      {showModal && selected && (
        <div className="fixed left-0 top-0 z-50 flex h-full w-full items-center justify-center bg-black/50">
          <div className="w-[640px] rounded bg-white p-6">
            <h4 className="text-lg font-black">Vincular {selected.fullName}</h4>

            <div className="mt-4 space-y-3">
              <label className="block">
                Tipo de relacionamento
                <select value={form.relationshipType} onChange={(e) => setForm({ ...form, relationshipType: e.target.value })} className="w-full rounded border px-2 py-2">
                  {Object.entries(relationshipLabels).map(([value, label]) => (
                    <option key={value} value={value}>{label}</option>
                  ))}
                </select>
              </label>

              <label className="flex items-center gap-2">
                <input type="checkbox" checked={form.canPickUpChild} onChange={(e) => setForm({ ...form, canPickUpChild: e.target.checked })} /> Pode buscar a criança
              </label>

              <label className="flex items-center gap-2">
                <input type="checkbox" checked={form.isFinanceContact} onChange={(e) => setForm({ ...form, isFinanceContact: e.target.checked })} /> Contato financeiro
              </label>

              <label className="flex items-center gap-2">
                <input type="checkbox" checked={form.isLegalResponsable} onChange={(e) => setForm({ ...form, isLegalResponsable: e.target.checked })} /> Responsável legal
              </label>

              <label>
                Observação
                <textarea value={form.observation} onChange={(e) => setForm({ ...form, observation: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <div className="flex gap-2">
                <button onClick={() => void handleCreateRelation()} className="rounded bg-green-600 px-3 py-2 text-white">Confirmar vínculo</button>
                <button onClick={() => setShowModal(false)} className="rounded bg-gray-200 px-3 py-2">Cancelar</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* create responsible modal */}
      {showCreateResponsible && (
        <div className="fixed left-0 top-0 z-50 flex h-full w-full items-center justify-center bg-black/50">
          <div className="w-[720px] rounded bg-white p-6">
            <h4 className="text-lg font-black">Cadastrar novo responsável</h4>
            <div className="mt-4 grid grid-cols-2 gap-3">
              <label className="col-span-2">
                Nome completo *
                <input value={responsibleForm.fullName} onChange={(e) => setResponsibleForm({ ...responsibleForm, fullName: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <label>
                CPF
                <input value={responsibleForm.cpf} onChange={(e) => setResponsibleForm({ ...responsibleForm, cpf: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <label>
                E-mail
                <input value={responsibleForm.email} onChange={(e) => setResponsibleForm({ ...responsibleForm, email: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <label>
                Telefone
                <input value={responsibleForm.phone} onChange={(e) => setResponsibleForm({ ...responsibleForm, phone: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <label>
                Profissão
                <input value={responsibleForm.profession} onChange={(e) => setResponsibleForm({ ...responsibleForm, profession: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>

              <label className="col-span-2">
                Endereço
                <input value={responsibleForm.address} onChange={(e) => setResponsibleForm({ ...responsibleForm, address: e.target.value })} className="w-full rounded border px-2 py-2" />
              </label>
            </div>

            <div className="mt-4 flex gap-2">
              <button onClick={() => void handleCreateResponsibleAndLink()} className="rounded bg-green-600 px-3 py-2 text-white">Criar e vincular</button>
              <button onClick={() => setShowCreateResponsible(false)} className="rounded bg-gray-200 px-3 py-2">Cancelar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}