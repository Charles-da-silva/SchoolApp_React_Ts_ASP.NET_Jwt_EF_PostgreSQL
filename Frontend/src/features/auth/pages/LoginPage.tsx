import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import logo from "../../../assets/turma-da-monica-logo.webp";

export default function LoginPage() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    localStorage.setItem("schoolapp:isAuthenticated", "true");
    navigate("/students");
  }

  return (
    <main className="grid min-h-screen place-items-center bg-[#fffaf2] px-4 py-10">
      <section className="w-full max-w-md rounded-lg border border-amber-100 bg-white p-8 shadow-sm">
        <Link to="/" className="mx-auto mb-8 flex w-fit flex-col items-center gap-3 text-center">
          <img src={logo} alt="Escola Infantil Turma da Monica" className="h-24 w-24 object-contain" />
          <span className="text-lg font-black text-red-600">Escola Infantil Turma da Monica</span>
        </Link>

        <h1 className="text-2xl font-black text-slate-950">Acessar sistema</h1>
        <p className="mt-2 text-sm text-slate-600">Entre para carregar a area administrativa com menu lateral.</p>

        <form onSubmit={handleSubmit} className="mt-8 space-y-5">
          <div>
            <label className="text-sm font-bold text-slate-700" htmlFor="email">E-mail</label>
            <input
              id="email"
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 text-slate-900 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              placeholder="admin@escola.com"
              required
            />
          </div>

          <div>
            <label className="text-sm font-bold text-slate-700" htmlFor="password">Senha</label>
            <input
              id="password"
              className="mt-2 w-full rounded-lg border border-slate-300 px-4 py-3 text-slate-900 outline-none transition focus:border-sky-600 focus:ring-4 focus:ring-sky-100"
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Digite sua senha"
              required
            />
          </div>

          <button className="w-full rounded-lg bg-red-600 px-5 py-3 font-black text-white transition hover:bg-red-700">
            Entrar
          </button>
        </form>
      </section>
    </main>
  );
}
