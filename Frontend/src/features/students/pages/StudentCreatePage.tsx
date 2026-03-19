import { useNavigate } from "react-router-dom"
import { StudentForm } from "../components/StudentForm"
import { createStudent } from "../services/StudentService"
import toast from "react-hot-toast"

export default function StudentCreatePage() {
  const navigate = useNavigate()

  async function handleCreate(data: any) {
    try {
      await createStudent(data)

      toast.success("Aluno criado com sucesso!")

      navigate("/students")
    } catch {
      toast.error("Erro ao criar aluno")
    }
  }

  return (
    <div>
      <h1>Novo Aluno</h1>

      <StudentForm onSubmit={handleCreate} />
    </div>
  )
}