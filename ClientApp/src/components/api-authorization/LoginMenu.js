import { Component, Fragment } from "react";
import { NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import authService from "../../services/AuthService";
import { connect } from "react-redux"; // Import connect
import { clearUser } from "../../features/user/userSlice";
import { logout } from "../../features/auth/authSlice";

export class LoginMenu extends Component {
  handleLogoutClick = () => {
    authService.logout();
  };

  render() {
    // These now come from props via mapStateToProps
    const { isAuthenticated } = this.props; // Using 'username' from Redux state
    const username = "";

    if (!isAuthenticated) {
      // const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;

      return this.authenticatedView(username, profilePath);
    }
  }

  authenticatedView(userName, profilePath) {
    return (
      <Fragment>
        <NavItem>
          <NavLink tag={Link} className="text-dark" to={profilePath}>
            Hello {userName}
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink
            tag="button"
            className="text-dark btn btn-link nav-link"
            onClick={this.handleLogoutClick}
          >
            Logout
          </NavLink>
        </NavItem>
      </Fragment>
    );
  }

  anonymousView(registerPath, loginPath) {
    return (
      <Fragment>
        {/* <NavItem>
          <NavLink tag={Link} className="text-dark" to={registerPath}>
            Register
          </NavLink>
        </NavItem> */}
        <NavItem>
          <NavLink tag={Link} className="text-dark" to={loginPath}>
            Login
          </NavLink>
        </NavItem>
      </Fragment>
    );
  }
}

const mapStateToProps = (state) => ({
  isAuthenticated: state.user.isAuthenticated, // Get from userSlice (or authSlice if that's where it is)
  username: state.user.username, // Get username from Redux state
});

const mapDispatchToProps = {
  clearUser, // Will be available as this.props.clearUser()
  logout,
};

export default connect(mapStateToProps, mapDispatchToProps)(LoginMenu);
