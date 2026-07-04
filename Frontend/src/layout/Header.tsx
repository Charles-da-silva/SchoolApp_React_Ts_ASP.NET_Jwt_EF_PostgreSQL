import { useNavigate } from "react-router-dom";
import logo from "../assets/turma-da-monica-logo.webp";
import { BrandLogoText } from "../shared/components/BrandLogoText";

export function Header() {
  const navigate = useNavigate();

  function handleLogout() {
    localStorage.removeItem("schoolapp:isAuthenticated");
    navigate("/");
  }

  return (
    <header className="flex min-h-20 items-center justify-between gap-4 bg-sky-700 px-6 text-white shadow-sm">
      <div className="flex items-center gap-3">
        <img src={logo} alt="Escola Infantil Turma da Mônica" className="h-14 w-14 rounded-full object-contain" />
        <div>
          <BrandLogoText size="header" />
          <span className="mt-1 block text-sm font-bold text-white">Sistema de Gestão Escolar</span>
        </div>
      </div>

      <button
        onClick={handleLogout}
        className="rounded-lg bg-white/15 px-4 py-2 text-sm font-bold transition hover:bg-white/25"
      >
        Sair
      </button>
    </header>
  );
}
