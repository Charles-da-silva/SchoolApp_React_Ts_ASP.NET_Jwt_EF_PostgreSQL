import { useNavigate } from "react-router-dom";
import { formatDateToPtBr } from "../../../shared/utils/dateUtils";
import type { Student } from "../types/Student";

function getDocumentTypeLabel(documentType: string) {
  return documentType === "2" || documentType === "BirthCertificateNumber" ? "Certidao de Nascimento" : "CPF";
}

type Props = {
  student: Student;
};

export function StudentCard({ student }: Props) {
  const navigate = useNavigate();

  function handleClick() {
    navigate(`/students/${student.id}`);
  }

  return (
    <div
      onClick={handleClick}
      className="cursor-pointer rounded-lg border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md"
    >
      <div className="mb-4 flex items-start justify-between gap-4">
        <strong className="text-slate-950">{student.fullName}</strong>
        <span
          className={`rounded-full px-3 py-1 text-xs font-black ${
            student.isActive ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"
          }`}
        >
          {student.isActive ? "Ativo" : "Inativo"}
        </span>
      </div>

      <div className="space-y-1 text-sm text-slate-600">
        <p>Documento: {getDocumentTypeLabel(student.documentType)}</p>
        <p>No. do documento: {student.documentNumber}</p>
        <p>Nascimento: {formatDateToPtBr(student.dateOfBirth)}</p>
      </div>
    </div>
  );
}
