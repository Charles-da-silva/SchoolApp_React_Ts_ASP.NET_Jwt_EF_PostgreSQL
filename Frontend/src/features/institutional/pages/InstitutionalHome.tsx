import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import logo from "../../../assets/turma-da-monica-logo.webp";
import logoVideo from "../../../assets/Logo_video.webm";
import { BrandLogoText } from "../../../shared/components/BrandLogoText";

const menuItems = ["Proposta", "Turmas", "Rotina", "Contato"];

export default function InstitutionalHome() {
  const [showHeroVideo, setShowHeroVideo] = useState(true);

  useEffect(() => {
    setShowHeroVideo(true);

    const fallbackTimer = window.setTimeout(() => {
      setShowHeroVideo(false);
    }, 10000);

    return () => window.clearTimeout(fallbackTimer);
  }, []);

  return (
    <div className="min-h-screen bg-[#fffaf2] text-slate-900">
      <header className="sticky top-0 z-20 border-b border-amber-100 bg-sky-700 backdrop-blur">
        <div className="mx-auto flex h-20 max-w-7xl items-center justify-between px-4 sm:px-6 lg:px-8">
          <Link to="/" className="flex items-center gap-3">
            <BrandLogoText size="header" />
          </Link>

          <nav className="hidden items-center brand-logo-text__prefix gap-8 text-lg font-bold text-slate-700 md:flex">
            {menuItems.map((item) => (
              <a key={item} href={`#${item.toLowerCase()}`} className="hover:text-red-600" style={{ padding: "30px" }}>
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
          <div className="rounded-xl align-center grid max-w-7xl gap-1 px-4 py-12 sm:px-6 lg:grid-cols-[1fr_0.85fr] lg:px-8 lg:py-8">
            <div className="rounded-xl align-center mx-30 w-90 ">
              {showHeroVideo ? (
                  <>
                  
                    <video
                      src={logoVideo}
                      className="mx-auto aspect-square h-110 w-90 object-cover object-center"
                      autoPlay
                      playsInline
                      onEnded={() => setShowHeroVideo(false)}
                    >
                      Seu navegador nÃo suporta vídeos.
                    </video>
                  
                  </>
                ) : (                  
                  <img src={logo} alt="Escola Infantil Turma da Mônica" className="h-94 w-94 object-contain" />
                )}
            </div>
            
            <div >
              <p className="mb-4 inline-flex rounded-full bg-yellow-300 px-4 py-2 text-lg font-black text-yellow-800">
                Educação infantil com acolhimento, brincadeira e rotina
              </p>
                              
              
              <p className="mt-6 max-w-2xl text-lg leading-8 text-slate-600">
                Um espaco colorido, seguro e afetivo para crianças aprenderem com autonomia,
                convivência e experiências pensadas para cada fase da infância.
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
          </div>
        </section>
        
        <section id="proposta" className="mx-auto max-w-7xl bg-yellow-300 rounded-xl py-14 sm:px-6 lg:px-8">
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
        <br />
        <section id="turmas" className="mx-auto  max-w-7xl bg-sky-400 rounded-xl py-14 sm:px-6 lg:px-8">
          <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <h2 className="text-3xl font-black">Turmas por fase</h2>
            <div className="mt-8 grid gap-4 md:grid-cols-4">
              {["Bercario", "Maternal", "Pre I", "Pre II"].map((item) => (
                <div key={item} className="rounded-lg bg-white p-5 font-black ring-1 ring-white/25">
                  {item}
                </div>
              ))}
            </div>
          </div>
        </section>
        <br />
        <section id="rotina" className="mx-auto max-w-7xl bg-red-300 rounded-xl px-4 py-14 sm:px-6 lg:px-8">
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
