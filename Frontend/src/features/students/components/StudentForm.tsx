import { useEffect, useState, type ChangeEvent, type FormEvent } from "react";
import type { StudentFormData } from "../types/StudentFormData";
import { useNavigate } from "react-router-dom";


type Props = {
  initialData?: StudentFormData;
  onSubmit: (data: StudentFormData) => Promise<void>;
};

export function StudentForm({ initialData, onSubmit }: Props) {
  const [form, setForm] = useState<StudentFormData>({
    fullName: "",
    documentType: "1",
    documentNumber: "",
    dateOfBirth: "",
    email: "",
  });

  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (initialData) {
      setForm(initialData);
    }
  }, [initialData]);

  function handleChange(e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) {
    setForm((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    try {
      setLoading(true);
      await onSubmit(form);
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="max-w-xl space-y-5 rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
      <div>
        <label className="text-sm font-bold text-slate-700">Nome completo</label>
        <input
          className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
          name="fullName"
          value={form.fullName}
          onChange={handleChange}
          required
        />
      </div>

      <div>
        <label className="text-sm font-bold text-slate-700">Tipo Documento</label>
        <select
          className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
          name="documentType"
          value={form.documentType}
          onChange={handleChange}
        >
          <option value="1">CPF</option>
          <option value="2">Certidao de Nascimento</option>
        </select>
      </div>

      <div>
        <label className="text-sm font-bold text-slate-700">Numero do documento</label>
        <input
          className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
          name="documentNumber"
          value={form.documentNumber}
          onChange={handleChange}
          required
        />
      </div>

      <div>
        <label className="text-sm font-bold text-slate-700">E-mail</label>
        <input
          className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
          type="email"
          name="email"
          value={form.email ?? ""}
          onChange={handleChange}
        />
      </div>

      <div>
        <label className="text-sm font-bold text-slate-700">Data de nascimento</label>
        <input
          className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
          type="date"
          name="dateOfBirth"
          value={form.dateOfBirth}
          onChange={handleChange}
          required
        />
      </div>
      <div className="mt-5 grid grid-cols-2 gap-2 border-t border-slate-100 pt-4">
      <button
        className="mt-6 rounded-lg bg-blue-500 px-5 py-3 font-black text-white transition hover:bg-blue-800"
        disabled={loading}
      >
        {loading ? "Salvando..." : "Salvar"}
      </button>
      <button
          className="mt-6 rounded-lg bg-white px-5 py-3 border border-slate-600 font-black text-black transition hover:bg-red-400"
          onClick={() => navigate("/students")}
        >
          Cancelar
        </button>
        </div>
    </form>
  );
}
