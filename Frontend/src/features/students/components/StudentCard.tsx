/* ------------------------------------------
Card é onde controlamos a exibição (visual) de cada aluno individualmente.
A página controla dados. O card controla visual.
Se você quiser mudar: layout para grid, layout para tabela, layout compacto,
adicionar botão, mudar cor, etc... tudo isso é responsabilidade do card.
---------------------------------------------*/

// Importamos o tipo Student
// Isso garante tipagem forte (TypeScript protege contra erros)
import type { Student } from "../types/Student";

import { formatDateToPtBr } from "../../../shared/utils/dateUtils";

// Aqui estamos dizendo:
// Esse componente recebe DUAS props:
// 1) student → dados do aluno
// 2) onDeactivate → função que será executada ao clicar
type StudentCardProps = {
  student: Student;
  onDeactivate: (id: string) => void;
};

// Aqui estamos "desestruturando" as props
// Isso significa que estamos pegando student e onDeactivate diretamente
export function StudentCard({ student, onDeactivate }: StudentCardProps) {
  return (
    <div
      style={{
        display: "grid",
        gridTemplateColumns: "1fr 1fr 1fr 1fr auto",
        border: "1px solid #ccc",
        padding: "10px 20px",
        marginBottom: "10px",
        borderRadius: "8px",
      }}
    >
      <p>
        <strong>Nome:</strong> {student.fullName}
      </p>

      <p>
        <strong>Email:</strong> {student.email}
      </p>

      <p>
        <strong>Status:</strong>{" "}
        {student.isActive ? "Ativo" : "Inativo"}
      </p>

      <p>
        <strong>Data de Nascimento:</strong> {formatDateToPtBr(student.dateOfBirth)}
      </p>

      <button
        onClick={() => onDeactivate(student.id)}
        style={{
          marginTop: "10px",
          padding: "5px 10px",
          cursor: "pointer",
        }}
      >
        Desativar
      </button>
    </div>
  );
}