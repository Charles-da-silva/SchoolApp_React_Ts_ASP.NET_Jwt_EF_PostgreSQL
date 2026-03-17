/*
Header = topo fixo da aplicação.
Nunca possui lógica de negócio.
Apenas layout global.
*/

import { colors, spacing } from "../shared/styles/tokens";

export function Header() {
  return (
    <header
      style={{
        height: "60px",
        background: colors.primary,
        color: "white",
        display: "flex",
        //position: "fixed",
        alignItems: "center",
        padding: `0 ${spacing.lg}`,
        fontWeight: "bold",
        fontSize: "30px"
      }}
    >
      🎓 School App
    </header>
  );
}