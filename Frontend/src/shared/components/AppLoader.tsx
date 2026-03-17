/*
Loader global da aplicação.

Responsável por:
✔ primeira experiência do usuário
✔ percepção de performance
✔ identidade visual inicial
*/

export function Loader() {
  return (
    <div
      style={{
        width: "100vw",
        height: "100vh",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        background: "#0f172a",
        color: "white",
        fontFamily: "sans-serif",
      }}
    >
      <h1 style={{ marginBottom: "20px" }}>🎓 School App</h1>

      {/* barra animada */}
      <div
        style={{
          width: "220px",
          height: "6px",
          background: "#1e293b",
          borderRadius: "10px",
          overflow: "hidden",
        }}
      >
        <div
          style={{
            height: "100%",
            width: "40%",
            background: "#38bdf8",
            animation: "loading 5.2s infinite",
          }}
        />
      </div>

      {/* animação CSS inline */}
      <style>
        {`
          @keyframes loading {
            0% { transform: translateX(-100%); }
            100% { transform: translateX(300%); }
          }
        `}
      </style>
    </div>
  );
}