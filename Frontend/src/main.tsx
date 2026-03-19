
import ReactDOM from "react-dom/client";
import App from "./App";
import { Toaster } from "react-hot-toast"

ReactDOM.createRoot(document.getElementById("root")!).render(
  <>
    <App/>

    <Toaster
        position="top-center"
        toastOptions={{
          duration: 3000,
        }}
      />
  </>
);
