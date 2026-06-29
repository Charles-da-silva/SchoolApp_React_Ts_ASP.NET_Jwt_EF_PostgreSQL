import { useNavigate, useParams } from "react-router-dom";
import { AppLoader } from "../../../shared/components/AppLoader";
import { formatDateToPtBr } from "../../../shared/utils/dateUtils";
import { useStudent } from "../hooks/useStudents";

function getDocumentTypeLabel(documentType: string) {
  return documentType === "2" || documentType === "BirthCertificateNumber" ? "Certidao de Nascimento" : "CPF";
}

export default function StudentDetailsPage() {
  const { id } = useParams();
  const { student, loading, error, reload } = useStudent(id);
  const navigate = useNavigate();

  if (loading) return <AppLoader />;

  if (error) {
    return (
      <div>
        <p className="mb-4 text-red-700">{error}</p>
        <button className="rounded-lg bg-sky-600 px-4 py-2 font-bold text-white" onClick={reload}>
          Tentar novamente
        </button>
      </div>
    );
  }

  if (!student) return <div className="text-slate-600">Aluno nao encontrado</div>;

  return (
    <div className="rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
      <h1 className="text-3xl font-black text-slate-950">{student.fullName}</h1>

      <div className="mt-6 space-y-3 text-slate-700">
        <p><strong>No. do documento:</strong> {student.documentNumber}</p>
        <p><strong>Tipo:</strong> {getDocumentTypeLabel(student.documentType)}</p>
        <p><strong>E-mail:</strong> {student.email || "Nao informado"}</p>
        <p><strong>Data de nascimento:</strong> {formatDateToPtBr(student.dateOfBirth)}</p>
        <p>
          <strong>Status:</strong>{" "}
          <span
            className={`rounded-full px-3 py-1 text-sm font-black ${
              student.isActive ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"
            }`}
          >
            {student.isActive ? "Ativo" : "Inativo"}
          </span>
        </p>
        <p><strong>Cadastrado em:</strong> {formatDateToPtBr(student.createdAt)}</p>
        {student.deactivatedAt ? (
          <p><strong>Desabilitado em:</strong> {formatDateToPtBr(student.deactivatedAt)}</p>
        ) : null}
      </div>

      <hr className="my-6 border-slate-200" />

      <h2 className="text-xl font-black text-slate-950">Ficha de anamnese</h2>
      {student.anamnesis ? (
        <p className="mt-3 text-slate-700">{student.anamnesis.content}</p>
      ) : (
        <p className="mt-3 text-slate-600">Nenhuma anamnese cadastrada.</p>
      )}

      <button
        className="mt-6 rounded-lg bg-red-600 px-5 py-3 font-black text-white transition hover:bg-red-700"
        onClick={() => navigate("edit")}
      >
        Editar
      </button>
    </div>
  );
}
