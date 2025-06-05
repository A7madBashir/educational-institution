import App from "../App";
import ReduxProvider from "./reduxProvider";

export default function AppProviders({ children }) {
  return <ReduxProvider>{children}</ReduxProvider>;
}
