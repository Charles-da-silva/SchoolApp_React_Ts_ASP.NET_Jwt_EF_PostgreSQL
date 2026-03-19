import { useParams, useNavigate } from "react-router-dom"
import { useStudent } from "../hooks/useStudents"
import { AppLoader } from "../../../shared/components/AppLoader"
import { formatDateToPtBr } from "../../../shared/utils/dateUtils";


export default function StudentDetailsPage() {
  const { id } = useParams()

  // retorno do hook (useStudens.ts)
  const { student, loading, error, reload } = useStudent(id)

  const navigate = useNavigate();

  if (loading) return <AppLoader />

  if (error)
    return (
      <div>
        {error}
        <button onClick={reload}>Tentar novamente</button>
      </div>
    )

  if (!student) return <div>Aluno não encontrado</div>

  return (
    <div style={{ padding: 24 }}>
      <h1>{student.fullName}</h1>

      <p><strong>Nº do documento:</strong> {student.documentNumber}</p>
      <p><strong>Tipo:</strong> {student.documentType}</p>
      <p><strong>Data de nascimento:</strong> {formatDateToPtBr(student.dateOfBirth)}</p>
      <p><strong>Status:</strong><span
          style={{
            background: student.isActive ? "#dcfce7" : "#fee2e2",
            color: student.isActive ? "#166534" : "#991b1b",
            padding: "4px 10px",
            borderRadius: "999px",
            fontWeight: 600,
          }}
        >
          {student.isActive ? "Ativo" : "Inativo"}
        </span>
      </p>
      <p><strong>Cadastrado em:</strong> {formatDateToPtBr(student.createdAt)}</p>
      {student.deactivatedAt ? (
        <p><strong>Desabilitado em:</strong> {formatDateToPtBr(student.deactivatedAt)}</p>
      ) : ("")}
      

      <hr />

      <h2>Ficha de anamnese</h2>

      {student.anamnesis ? (
        <p>{student.anamnesis.content}</p>
      ) : (
        <p>Nenhuma anamnese cadastrada.</p>
      )}

      <button onClick={() => navigate('edit')}>
        Editar
      </button>

    </div>
    
  )
}