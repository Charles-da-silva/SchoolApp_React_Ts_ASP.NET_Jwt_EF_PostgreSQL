import toast from "react-hot-toast";
import { useNavigate, useParams } from "react-router-dom";
import { AppLoader } from "../../../shared/components/AppLoader";
import { StudentForm } from "../components/StudentForm";
import { useStudent } from "../hooks/useStudents";
import { updateStudent } from "../services/StudentService";
import type { StudentFormData } from "../types/StudentFormData";

export default function StudentEditPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { student, loading } = useStudent(id);

  if (loading || !student) return <AppLoader />;

  async function handleUpdate(data: StudentFormData) {
    try {
      await updateStudent(id!, data);
      toast.success("Aluno atualizado!");
      navigate(`/students/${id}`);
    } catch {
      toast.error("Erro ao atualizar aluno");
    }
  }

  return (
    <div>
      <h1 className="mb-5 text-3xl font-black text-slate-950">Editar Aluno</h1>
      <StudentForm
        initialData={{
          fullName: student.fullName,
          documentType: student.documentType,
          documentNumber: student.documentNumber,
          email: student.email ?? "",
          dateOfBirth: student.dateOfBirth,
        }}
        onSubmit={handleUpdate}
      />
    </div>
  );
}
