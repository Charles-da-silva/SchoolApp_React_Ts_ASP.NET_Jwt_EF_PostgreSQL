import { useParams, useNavigate } from "react-router-dom"
import { StudentForm } from "../components/StudentForm"
import { useStudent } from "../hooks/useStudents"
import { updateStudent } from "../services/StudentService"
import { Loader } from "../../../shared/components/AppLoader"

export default function StudentEditPage() {
  const { id } = useParams()
  const navigate = useNavigate()

  const { student, loading } = useStudent(id)

  if (loading || !student) return <Loader />

  async function handleUpdate(data: any) {
    await updateStudent(id!, data)
    navigate(`/students/${id}`)
  }

  return (
    <div>
      <h1>Editar Aluno</h1>

      <StudentForm
        initialData={{
          fullName: student.fullName,
          documentType: student.documentType,
          documentNumber: student.documentNumber,
          dateOfBirth: student.dateOfBirth,
        }}
        onSubmit={handleUpdate}
      />
    </div>
  )
}