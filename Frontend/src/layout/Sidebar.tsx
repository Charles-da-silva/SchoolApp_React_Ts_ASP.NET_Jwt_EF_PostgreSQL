/*
Sidebar = navegação principal do sistema.
Depois ligaremos com React Router.
*/

//NavLink sabe qual rota está ativa.
import { NavLink } from "react-router-dom";

import { colors, spacing, radius } from "../shared/styles/tokens";

export function Sidebar() {
    const linkStyle = ({ isActive }: { isActive: boolean }) => ({
    display: "block",
    padding: spacing.md,
    color: "white",
    textDecoration: "none",
    background: isActive ? colors.primary : "transparent",
    borderRadius: radius.sm,
  });

  return (
     <aside
      style={{
        width: "220px",
        background: colors.primaryDark,
        padding: spacing.lg,
        minHeight: "100vh",
      }}
    >
      <h3 style={{ color: "white" }}>Menu</h3>

      <nav>
        <NavLink to="/students" style={linkStyle}>
          👨‍🎓 Alunos
        </NavLink>
      </nav>
    </aside>
  );
}