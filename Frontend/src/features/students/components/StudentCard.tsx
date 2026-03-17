/* ------------------------------------------
Card é onde controlamos a exibição (visual) de cada aluno individualmente.
A página (StudentPage) controla dados. O card controla visual.
Se você quiser mudar: layout para grid, layout para tabela, layout compacto,
adicionar botão, mudar cor, etc... tudo isso é responsabilidade do card.

Responsabilidades:
✔ exibir aluno
✔ layout consistente
❌ não buscar dados
❌ não controlar estado global
---------------------------------------------*/

// Importamos o tipo Student
// Isso garante tipagem forte (TypeScript protege contra erros)
import type { Student } from "../types/Student";

import { formatDateToPtBr } from "../../../shared/utils/dateUtils";

import { colors, spacing, radius, shadows } from "../../../shared/styles/tokens";

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
        background: colors.surface,
        padding: spacing.lg,
        borderRadius: radius.md,
        boxShadow: shadows.sm,
        border: `1px solid ${colors.border}`,
        transition: "all 0.2s ease",
        cursor: "pointer",
      }}
      onMouseEnter={(e) =>
        (e.currentTarget.style.boxShadow = shadows.md)
      }
      onMouseLeave={(e) =>
        (e.currentTarget.style.boxShadow = shadows.sm)
      }
    >
      {/* HEADER */}
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          marginBottom: spacing.md,
        }}
      >
        <strong style={{ color: colors.text }}>
          {student.fullName}
        </strong>

        {/* STATUS BADGE */}
        <span
          style={{
            background: student.isActive ? "#dcfce7" : "#fee2e2",
            color: student.isActive ? "#166534" : "#991b1b",
            padding: "4px 10px",
            borderRadius: "999px",
            fontSize: "12px",
            fontWeight: 600,
          }}
        >
          {student.isActive ? "Ativo" : "Inativo"}
        </span>
      </div>

      {/* BODY */}
      <div style={{ color: colors.textLight, fontSize: "14px" }}>
        <p>
          Documento:{" "}
          {student.documentType === "BirthCertificateNumber" ? "Certidão de Nascimento" : "CPF"}
        </p>
        <p>Nº do documento: {student.documentNumber}</p>
        <p>Nascimento: {formatDateToPtBr(student.dateOfBirth)}</p>
      </div>
    </div>
  );
}