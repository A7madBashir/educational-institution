import { Component } from "react";
import { Navigate } from "react-router-dom";
import authService from "../../services/AuthService";

export default class AuthorizeRoute extends Component {
  constructor(props) {
    super(props);

    this.state = {
      ready: false,
      authenticated: false,
    };
  }

  componentDidMount() {
    this.populateAuthenticationState();
  }

  render() {
    const { ready, authenticated } = this.state;
    var link = document.createElement("a");
    link.href = this.props.path;
    const returnUrl = ``;
    const redirectUrl = `/authentication/login`;
    if (!ready) {
      return <div></div>;
    } else {
      const { element } = this.props;
      return authenticated ? element : <Navigate replace to={redirectUrl} />;
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ ready: true, authenticated });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}
