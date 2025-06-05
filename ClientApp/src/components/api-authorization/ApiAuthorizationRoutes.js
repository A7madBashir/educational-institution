import { Login } from "./Login";
import { ApplicationPaths, LoginActions } from "./ApiAuthorizationConstants";

const ApiAuthorizationRoutes = [
  {
    path: ApplicationPaths.Login,
    element: loginAction(LoginActions.Login),
  },
  {
    path: ApplicationPaths.LoginFailed,
    element: loginAction(LoginActions.LoginFailed),
  },
  {
    path: ApplicationPaths.LoginCallback,
    element: loginAction(LoginActions.LoginCallback),
  },
  {
    path: ApplicationPaths.Profile,
    element: loginAction(LoginActions.Profile),
  },
  {
    path: ApplicationPaths.Register,
    element: loginAction(LoginActions.Register),
  },
  {
    path: ApplicationPaths.LogOut,
    element: loginAction(LoginActions.LogOut),
  },
];

function loginAction(name) {
  return <Login action={name}></Login>;
}

export default ApiAuthorizationRoutes;
