import { Navigate, createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../layout/MainLayout";
import StudentsPage from "../../features/students/pages/StudentPage";
import StudentDetailsPage from "../../features/students/pages/StudentDetailsPage";
import ResponsibleListPage from "../../features/students/pages/ResponsibleListPage";
import ResponsibleDetailsPage from "../../features/students/pages/ResponsibleDetailsPage";
import StudentEditPage from "../../features/students/pages/StudentEditPage";
import StudentCreatePage from "../../features/students/pages/StudentCreatePage";
import StudentSearchPage from "../../features/students/pages/StudentSearchPage";
import InstitutionalHome from "../../features/institutional/pages/InstitutionalHome";
import LoginPage from "../../features/auth/pages/LoginPage";
import { ProtectedRoute } from "../../features/auth/components/ProtectedRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <InstitutionalHome />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        element: <MainLayout />,
        children: [
          {
            path: "/app",
            element: <Navigate to="/students" replace />,
          },
          {
            path: "/students",
            element: <StudentsPage />,
          },
          {
            path: "/students/new",
            element: <StudentCreatePage />,
          },
          {
            path: "/students/search",
            element: <StudentSearchPage />,
          },
          {
            path: "/students/:id",
            element: <StudentDetailsPage />,
          },
          {
            path: "/students/:id/edit",
            element: <StudentEditPage />,
          },
          {
            path: "/responsibles",
            element: <ResponsibleListPage />,
          },
          {
            path: "/responsibles/:id",
            element: <ResponsibleDetailsPage />,
          },
        ],
      },
    ],
  },
]);
