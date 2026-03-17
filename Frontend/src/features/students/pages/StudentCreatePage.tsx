import { useNavigate } from "react-router-dom"
import { StudentForm } from "../components/StudentForm"
import { createStudent } from "../services/StudentService"

export default function StudentCreatePage() {
  const navigate = useNavigate()

  async function handleCreate(data: any) {
    await createStudent(data)
    navigate("/students")
  }

  return (
    <div>
      <h1>Novo Aluno</h1>

      <StudentForm onSubmit={handleCreate} />
    </div>
  )
}