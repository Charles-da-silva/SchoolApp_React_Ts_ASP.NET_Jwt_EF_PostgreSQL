import { useNavigate } from "react-router-dom";

export function ResponsibleCard({ responsible }: { responsible: any }) {
  const navigate = useNavigate();

  function handleClick() {
    navigate(`/responsibles/${responsible.id}`);
  }

  return (
    <div
      onClick={handleClick}
      className="cursor-pointer rounded-lg border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md"
    >
      <div className="mb-4 flex items-start justify-between gap-4">
        <strong className="text-slate-950">{responsible.fullName}</strong>
        <span className={`rounded-full px-3 py-1 text-xs font-black ${responsible.isActive ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'}`}>
          {responsible.isActive ? 'Ativo' : 'Inativo'}
        </span>
      </div>

      <div className="space-y-1 text-sm text-slate-600">
        <p>CPF: {responsible.cpf || '—'}</p>
        <p>E-mail: {responsible.email || '—'}</p>
        <p>Telefone: {responsible.phone || '—'}</p>
      </div>
    </div>
  );
}
