import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { StudentForm } from "../components/StudentForm";
import { createStudent } from "../services/StudentService";
import type { StudentFormData } from "../types/StudentFormData";

export default function StudentCreatePage() {
  const navigate = useNavigate();

  async function handleCreate(data: StudentFormData) {
    try {
      await createStudent(data);
      toast.success("Aluno criado com sucesso!");
      navigate("/students");
    } catch {
      toast.error("Erro ao criar aluno");
    }
  }

  return (
    <div>
      <h1 className="mb-5 text-3xl font-black text-slate-950">Novo Aluno</h1>
      <StudentForm onSubmit={handleCreate} />
    </div>
  );
}
