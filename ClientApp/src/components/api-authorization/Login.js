import { Component } from "react";
import LoginForm from "../../modules/auth/components/login";
import authService from "../../services/AuthService";
import { Navigate } from "react-router-dom";
import { LoginActions } from "./ApiAuthorizationConstants";

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
export class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      action: props.action,
      message: undefined,
      isAuthenticated: false,
    };
  }

  componentDidMount() {
    this.populateAuthenticationState();
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ isAuthenticated: authenticated, message: undefined });
  }

  render() {
    if (
      this.state.action == LoginActions.LogOut &&
      this.state.isAuthenticated
    ) {
      console.log(this.state.action);
      authService.logout();
    }

    if (this.state.isAuthenticated) return <Navigate replace to={"/"} />;
    return (
      <div>
        <LoginForm />
      </div>
    );
  }
}
