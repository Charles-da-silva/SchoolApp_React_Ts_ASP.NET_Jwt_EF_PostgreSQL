import { createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../layout/MainLayout";
import StudentsPage from "../../features/students/pages/StudentPage";
import StudentDetailsPage from "../../features/students/pages/StudentDetailsPage";

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
          index: true, // rota padrão
          element: <StudentsPage />,
        },
        {
          path: "/students", // mesmo endereço da GET/api/Students (http://localhost:5173/students)
          element: <StudentsPage />,
        },
        {
          path: "students/:id", // GET/api/Students/{id}
          element: <StudentDetailsPage />,

          /*
          O que é o :id
            React Router cria parâmetros:
            /students/a42676c4
                      ↑
                      param
            Depois acessamos via useParams().
          */
        },
    ],
  },
]);