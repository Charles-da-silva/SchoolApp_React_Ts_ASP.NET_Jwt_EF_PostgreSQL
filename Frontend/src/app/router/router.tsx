import { createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../layout/MainLayout";
import StudentsPage from "../../features/students/pages/StudentPage";

/*
Router define:
- rotas
- layout base
- páginas
*/

export const router = createBrowserRouter([
  {
    path: "/",
    element: <MainLayout />,
    children: [
        {
        index: true, // ⭐ rota padrão
        element: <StudentsPage />,
        },
        {
            path: "/students",
            element: <StudentsPage />,
        },
    ],
  },
]);