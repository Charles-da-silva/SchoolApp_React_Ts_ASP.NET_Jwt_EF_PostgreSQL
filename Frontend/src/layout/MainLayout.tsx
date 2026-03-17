import { Header } from "./Header";
import { Sidebar } from "./Sidebar";
import { Outlet } from "react-router-dom";
import { colors, spacing } from "../shared/styles/tokens";

// MainLayout = casca da aplicação.


export function MainLayout() {
  return (
    <div style={{
        width: "calc(98vw)", 
        background: "white", padding: 0, margin: 0}}>
      <Header />

      <div style={{ 
        display: "flex",}}>
        <Sidebar />

        <main
            style={{
                flex: 1,
                padding: spacing.lg,
                background: colors.background,
                minHeight: "calc(100vh - 60px)",
            }}
        >
          {/* Router renderiza página aqui */}
          <Outlet />
        </main>
      </div>
    </div>
  );
}