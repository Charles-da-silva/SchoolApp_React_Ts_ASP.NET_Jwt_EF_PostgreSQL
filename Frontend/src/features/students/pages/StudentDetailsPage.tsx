import { useParams, useNavigate } from "react-router-dom"
import { useStudent } from "../hooks/useStudents"
import { Loader } from "../../../shared/components/AppLoader"


export default function StudentDetailsPage() {
  const { id } = useParams()

  const { student, loading, error, reload } = useStudent(id)

  const navigate = useNavigate();

  if (loading) return <Loader />

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
      <p><strong>Data nascimento:</strong> {student.dateOfBirth}</p>
      <p><strong>Status:</strong> {student.isActive ? "Ativo" : "Inativo"}</p>
      <p><strong>Aluno cadastrado em:</strong> {student.createdAt}</p>
      {student.deactivatedAt ? (
        <p><strong>Aluno desabilitado em:</strong> {student.deactivatedAt}</p>
      ) : ("")}
      

      <hr />

      <h2>Anamnese</h2>

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