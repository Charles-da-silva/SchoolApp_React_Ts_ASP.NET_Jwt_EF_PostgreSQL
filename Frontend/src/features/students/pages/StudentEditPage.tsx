import { useParams, useNavigate } from "react-router-dom"
import { StudentForm } from "../components/StudentForm"
import { useStudent } from "../hooks/useStudents"
import { updateStudent } from "../services/StudentService"
import { AppLoader } from "../../../shared/components/AppLoader"
import toast from "react-hot-toast"

export default function StudentEditPage() {
  const { id } = useParams()
  const navigate = useNavigate()

  const { student, loading } = useStudent(id)

  if (loading || !student) return <AppLoader />

  async function handleUpdate(data: any) {
    try {
      await updateStudent(id!, data)

      toast.success("Aluno atualizado!")

      navigate(`/students/${id}`)
    } catch {
      toast.error("Erro ao atualizar aluno")
    }
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