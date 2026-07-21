import { NavLink } from "react-router-dom";

export function Sidebar() {
  return (
    <aside className="min-h-[calc(100vh-64px)] w-64 bg-slate-950 p-5 text-white">
      <h3 className="mb-5 text-sm font-black uppercase tracking-wide text-slate-400">Menu</h3>

      <nav className="space-y-2">
        <NavLink
          to="/students"
          className={({ isActive }) =>
            `block rounded-lg px-4 py-3 font-bold transition ${
              isActive ? "bg-sky-600 text-white" : "text-slate-300 hover:bg-white/10 hover:text-white"
            }`
          }
        >
          Alunos
        </NavLink>

        <NavLink
          to="/responsibles"
          className={({ isActive }) =>
            `block rounded-lg px-4 py-3 font-bold transition ${
              isActive ? "bg-sky-600 text-white" : "text-slate-300 hover:bg-white/10 hover:text-white"
            }`
          }
        >
          Responsáveis
        </NavLink>
      </nav>
    </aside>
  );
}
