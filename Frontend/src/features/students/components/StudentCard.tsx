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
// Esse componente recebe a seguinte props:
// 1) student → dados do aluno
type Props = {
  student: Student;
};

// Aqui estamos "desestruturando" as props
// Isso significa que estamos pegando student
export function StudentCard({ student }: Props) {
  return (
    <div
      style={{
        display: "box",
        border: "1px solid #e5e7eb",
        borderRadius: "10px",
        padding: "16px",
        background: "#050505",
        boxShadow: "0 2px 6px rgba(0,0,0,0.05)",
      }}
    >
      <h3 style={{ marginBottom: "8px" }}>
        {student.fullName}
      </h3>

      <p>
        <strong>Documento:</strong>{" "}
        {student.documentType === "BirthCertificateNumber" ? "Certidão de Nascimento" : "CPF"}
      </p>

      <p>
        <strong>Número:</strong>{" "}
        {student.documentNumber}
      </p>

      <p>
        <strong>Data de Nascimento:</strong> {formatDateToPtBr(student.dateOfBirth)}
      </p>

      <p>
        <strong>Status do aluno:</strong>{" "}
        {student.isActive ? "Ativo ✅" : "Inativo ❌"}
      </p>

    </div>
  );
}