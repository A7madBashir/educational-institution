import ApiAuthorizationRoutes from "../components/api-authorization/ApiAuthorizationRoutes";
import HomePage from "../modules/home/components/home";
import NotFoundPage from "../components/not-found";

const AppRoutes = [
  {
    index: true,
    requireAuth: true,
    element: <HomePage />,
  },
  {
    path: "*",
    requireAuth: false,
    element: <NotFoundPage />,
  },
  ...ApiAuthorizationRoutes,
];

export default AppRoutes;
