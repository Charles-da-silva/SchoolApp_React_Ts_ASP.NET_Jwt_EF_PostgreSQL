import { useNavigate, useParams } from "react-router-dom";
import { AppLoader } from "../../../shared/components/AppLoader";
import { formatDateToPtBr } from "../../../shared/utils/dateUtils";
import { useStudent } from "../hooks/useStudents";
import { useState } from "react";
import toast from "react-hot-toast";
import { deactivateStudent, deleteStudent, reactivateStudent } from "../services/StudentService";


function getDocumentTypeLabel(documentType: string) {
  return documentType === "2" || documentType === "BirthCertificateNumber" ? "Certidao de Nascimento" : "CPF";
}

export default function StudentDetailsPage() {
  const { id } = useParams();
  const { student, loading, error, reload } = useStudent(id);
  const navigate = useNavigate();

  const [actionLoading, setActionLoading] = useState(false);

  async function handleDeactivate() {
    if (!student) return;

    try {
      setActionLoading(true);
      await deactivateStudent(student.id);
      toast.success("Aluno desativado.");
      await reload();
    } catch {
      toast.error("Nao foi possivel desativar o aluno.");
    } finally {
      setActionLoading(false);
    }
  }

  async function handleReactivate() {
    if (!student) return;

    try {
      setActionLoading(true);
      await reactivateStudent(student.id);
      toast.success("Aluno reativado.");
      await reload();
    } catch {
      toast.error("Nao foi possivel reativar o aluno.");
    } finally {
      setActionLoading(false);
    }
  }

  async function handleDelete() {
    if (!student) return;

    const confirmed = window.confirm("Excluir definitivamente este aluno?");
    if (!confirmed) return;

    try {
      setActionLoading(true);
      await deleteStudent(student.id);
      toast.success("Aluno excluido.");
      navigate("/students");
    } catch {
      toast.error("Não foi possível excluir o aluno.");
    } finally {
      setActionLoading(false);
    }
  }

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

  if (!student) return <div className="text-slate-600">Aluno não encontrado.</div>;

  return (
    <div className="rounded-lg border border-slate-200 bg-white p-6 shadow-sm">
      
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h1 className="text-3xl font-black text-slate-950">{student.fullName}</h1>
        <button
          className="rounded-lg border border-slate-300 bg-white px-5 py-3 font-black text-slate-700 transition hover:border-sky-500 hover:text-sky-700"
          onClick={() => navigate("/students")}
        >
          Voltar
        </button>
      </div>
      
      <div className="mt-6 space-y-3 text-slate-700">
        <p><strong>Nº do documento:</strong> {student.documentNumber}</p>
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

      <div className="mt-5 grid grid-cols-3 gap-2 border-t border-slate-100 pt-4">
        

        <button
          className="mt-6 rounded-lg bg-green-600 px-5 py-3 font-black text-white transition hover:bg-green-700"
          onClick={() => navigate("edit")}
        >
          Editar
        </button>

        {student.isActive ? (
          <button
            type="button"
            disabled={actionLoading}
            className="mt-6 rounded-lg bg-amber-500 px-5 py-3 font-black text-white transition hover:bg-amber-600"
            onClick={(event) => {
              event.stopPropagation();
              void handleDeactivate();
            }}
          >
            Desativar
          </button>
        ) : (
          <button
            type="button"
            disabled={actionLoading}
            className="mt-6 rounded-lg bg-blue-500 px-5 py-3 font-black text-white transition hover:bg-blue-600"
            onClick={(event) => {
              event.stopPropagation();
              void handleReactivate();
            }}
          >
            Reativar
          </button>
        )}
        <button
          type="button"
          disabled={actionLoading}
          className="mt-6 rounded-lg bg-red-500 px-5 py-3 font-black text-white transition hover:bg-red-800"
          onClick={(event) => {
            event.stopPropagation();
            void handleDelete();
          }}
        >
          Excluir
        </button>
      </div>
      
    </div>
  );
}
