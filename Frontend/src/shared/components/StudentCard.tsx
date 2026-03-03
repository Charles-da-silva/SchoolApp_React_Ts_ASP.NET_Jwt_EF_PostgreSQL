/* ------------------------------------------
Card é onde controlamos a exibição (visual) de cada aluno individualmente.
A página controla dados. O card controla visual.
Se você quiser mudar: layout para grid, layout para tabela, layout compacto,
adicionar botão, mudar cor, etc... tudo isso é responsabilidade do card.
---------------------------------------------*/

// Importamos o tipo Student
// Isso garante tipagem forte (TypeScript protege contra erros)
import type { Student } from "../../features/students/types/Student";

// Criamos um tipo para as propriedades (props) do componente
// Props são dados que um componente recebe
type StudentCardProps = {
  student: Student;
};

// Componente funcional
// Ele recebe props e retorna JSX (HTML do React)
export function StudentCard({ student }: StudentCardProps) {
  return (
    <div
      style={{
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
        <strong>Data de Nascimento:</strong> {student.dateOfBirth}
      </p>

      <p>
        <strong>Status:</strong>{" "}
        {student.isActive ? "Ativo" : "Inativo"}
      </p>
    </div>
  );
}