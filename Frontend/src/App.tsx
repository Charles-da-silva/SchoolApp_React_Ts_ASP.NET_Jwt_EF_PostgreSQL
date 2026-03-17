import { useEffect, useState } from "react";
import { Loader } from "./shared/components/AppLoader";
import { RouterProvider } from "react-router-dom";
import { router } from "./app/router/router";

/*
App = ponto raiz.
Aqui definimos layout global.
*/

export default function App() {
  const [isBooting, setIsBooting] = useState(true);

  // simula inicialização do app
  useEffect(() => {
    const timer = setTimeout(() => {
      setIsBooting(false);
    }, 1200); // tempo do splash

    return () => clearTimeout(timer);
  }, []);

  // ⭐ tela inicial
  if (isBooting) {
    return <Loader />;
  }

  // ⭐ app real
  return (
    
      <RouterProvider router={router} />
    
  );
}