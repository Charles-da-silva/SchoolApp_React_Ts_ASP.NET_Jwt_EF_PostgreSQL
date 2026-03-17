import { useEffect, useState } from "react"
import type { StudentFormData } from "../types/StudentFormData"

type Props = {
  initialData?: StudentFormData
  onSubmit: (data: StudentFormData) => Promise<void>
}

/**
 * Form reutilizável para CREATE e EDIT.
 *
 * - Se receber initialData → modo edição
 * - Se não → modo criação
 */
export function StudentForm({ initialData, onSubmit }: Props) {
  const [form, setForm] = useState<StudentFormData>({
    fullName: "",
    documentType: "",
    documentNumber: "",
    dateOfBirth: "",
  })

  const [loading, setLoading] = useState(false)

  // ⭐ preenche form quando vem dados do edit
  useEffect(() => {
    if (initialData) {
      setForm(initialData)
    }
  }, [initialData])

  function handleChange(
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) {
    setForm(prev => ({
      ...prev,
      [e.target.name]: e.target.value,
    }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()

    try {
      setLoading(true)
      await onSubmit(form)
    } finally {
      setLoading(false)
    }
  }

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: 500 }}>
      <div>
        <label>Nome completo</label>
        <input
          name="fullName"
          value={form.fullName}
          onChange={handleChange}
          required
        />
      </div>

      <div>
        <label>Tipo Documento</label>
        <select
          name="documentType"
          value={form.documentType}
          onChange={handleChange}
        >
          <option value="CPF">CPF</option>
          <option value="BirthCertificateNumber">
            Certidão de Nascimento
          </option>
        </select>
      </div>

      <div>
        <label>Número do documento</label>
        <input
          name="documentNumber"
          value={form.documentNumber}
          onChange={handleChange}
          required
        />
      </div>

      <div>
        <label>Data de nascimento</label>
        <input
          type="date"
          name="dateOfBirth"
          value={form.dateOfBirth}
          onChange={handleChange}
          required
        />
      </div>

      <button disabled={loading}>
        {loading ? "Salvando..." : "Salvar"}
      </button>
    </form>
  )
}