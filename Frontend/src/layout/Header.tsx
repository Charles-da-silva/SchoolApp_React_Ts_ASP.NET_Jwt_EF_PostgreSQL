import { useNavigate } from "react-router-dom";

export function Header() {
  const navigate = useNavigate();

  function handleLogout() {
    localStorage.removeItem("schoolapp:isAuthenticated");
    navigate("/");
  }

  return (
    <header className="flex h-16 items-center justify-between bg-sky-700 px-6 text-white shadow-sm">
      <strong className="text-xl font-black">School App</strong>
      <button
        onClick={handleLogout}
        className="rounded-lg bg-white/15 px-4 py-2 text-sm font-bold transition hover:bg-white/25"
      >
        Sair
      </button>
    </header>
  );
}
