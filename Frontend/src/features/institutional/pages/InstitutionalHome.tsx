import { Link } from "react-router-dom";
import logo from "../../../assets/turma-da-monica-logo.webp";

const menuItems = ["Proposta", "Turmas", "Rotina", "Contato"];

export default function InstitutionalHome() {
  return (
    <div className="min-h-screen bg-[#fffaf2] text-slate-900">
      <header className="sticky top-0 z-20 border-b border-amber-100 bg-white/95 backdrop-blur">
        <div className="mx-auto flex h-20 max-w-7xl items-center justify-between px-4 sm:px-6 lg:px-8">
          <Link to="/" className="flex items-center gap-3">
            <img src={logo} alt="Escola Infantil Turma da Monica" className="h-14 w-14 rounded-full object-contain" />
            <div>
              <strong className="block text-base font-black text-red-600 sm:text-lg">Escola Infantil</strong>
              <span className="text-sm font-bold text-sky-700">Turma da Monica</span>
            </div>
          </Link>

          <nav className="hidden items-center gap-8 text-sm font-bold text-slate-700 md:flex">
            {menuItems.map((item) => (
              <a key={item} href={`#${item.toLowerCase()}`} className="hover:text-red-600">
                {item}
              </a>
            ))}
          </nav>

          <Link to="/login" className="rounded-lg bg-red-600 px-5 py-3 text-sm font-black text-white shadow-sm transition hover:bg-red-700">
            Login
          </Link>
        </div>
      </header>

      <main>
        <section className="border-b border-amber-100 bg-white">
          <div className="mx-auto grid max-w-7xl items-center gap-10 px-4 py-12 sm:px-6 lg:grid-cols-[1fr_0.85fr] lg:px-8 lg:py-16">
            <div>
              <p className="mb-4 inline-flex rounded-full bg-yellow-100 px-4 py-2 text-sm font-black text-yellow-800">
                Educacao infantil com acolhimento, brincadeira e rotina
              </p>
              <h1 className="max-w-3xl text-4xl font-black leading-tight text-slate-950 sm:text-5xl lg:text-6xl">
                Escola Infantil Turma da Monica
              </h1>
              <p className="mt-6 max-w-2xl text-lg leading-8 text-slate-600">
                Um espaco colorido, seguro e afetivo para criancas aprenderem com autonomia,
                convivencia e experiencias pensadas para cada fase da infancia.
              </p>
              <div className="mt-8 flex flex-wrap gap-3">
                <a href="#contato" className="rounded-lg bg-sky-600 px-6 py-3 text-sm font-black text-white shadow-sm transition hover:bg-sky-700">
                  Agendar visita
                </a>
                <a href="#proposta" className="rounded-lg border border-slate-300 bg-white px-6 py-3 text-sm font-black text-slate-800 transition hover:border-red-300 hover:text-red-700">
                  Conhecer proposta
                </a>
              </div>
            </div>

            <div className="rounded-lg border border-amber-100 bg-[#fff4d8] p-6 shadow-sm">
              <img src={logo} alt="Logo Turma da Monica" className="mx-auto max-h-80 w-full object-contain" />
            </div>
          </div>
        </section>

        <section id="proposta" className="mx-auto max-w-7xl px-4 py-14 sm:px-6 lg:px-8">
          <div className="grid gap-4 md:grid-cols-3">
            {[
              ["Acolhimento", "Adaptacao respeitosa e vinculo proximo com as familias."],
              ["Aprendizagem", "Projetos, historias, movimento, artes e investigacao diaria."],
              ["Seguranca", "Rotina organizada, ambientes preparados e equipe atenta."],
            ].map(([title, text]) => (
              <article key={title} className="rounded-lg border border-amber-100 bg-white p-6 shadow-sm">
                <h2 className="text-xl font-black text-slate-950">{title}</h2>
                <p className="mt-3 leading-7 text-slate-600">{text}</p>
              </article>
            ))}
          </div>
        </section>

        <section id="turmas" className="bg-sky-700 py-14 text-white">
          <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <h2 className="text-3xl font-black">Turmas por fase</h2>
            <div className="mt-8 grid gap-4 md:grid-cols-4">
              {["Bercario", "Maternal", "Pre I", "Pre II"].map((item) => (
                <div key={item} className="rounded-lg bg-white/12 p-5 font-black ring-1 ring-white/25">
                  {item}
                </div>
              ))}
            </div>
          </div>
        </section>

        <section id="rotina" className="mx-auto max-w-7xl px-4 py-14 sm:px-6 lg:px-8">
          <div className="grid gap-8 lg:grid-cols-[0.7fr_1fr]">
            <h2 className="text-3xl font-black text-slate-950">Uma rotina clara para a crianca se sentir segura.</h2>
            <div className="grid gap-3 sm:grid-cols-2">
              {["Chegada acolhedora", "Roda de conversa", "Atividades dirigidas", "Parque e movimento"].map((item) => (
                <div key={item} className="rounded-lg border border-amber-100 bg-white p-5 font-bold text-slate-700">
                  {item}
                </div>
              ))}
            </div>
          </div>
        </section>

        <section id="contato" className="border-t border-amber-100 bg-white py-10">
          <div className="mx-auto flex max-w-7xl flex-col gap-4 px-4 sm:px-6 md:flex-row md:items-center md:justify-between lg:px-8">
            <div>
              <h2 className="text-2xl font-black text-slate-950">Venha conhecer a escola</h2>
              <p className="mt-2 text-slate-600">Atendimento de segunda a sexta, com visitas agendadas.</p>
            </div>
            <Link to="/login" className="w-full rounded-lg bg-red-600 px-6 py-3 text-center text-sm font-black text-white transition hover:bg-red-700 md:w-auto">
              Acessar sistema
            </Link>
          </div>
        </section>
      </main>
    </div>
  );
}
