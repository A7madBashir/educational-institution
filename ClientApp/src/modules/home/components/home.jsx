// client-app/src/pages/HomePage.js (or .tsx)
import React from "react";
import { Link, Navigate } from "react-router-dom";
import { useSelector } from "react-redux"; // <--- Import useSelector
import { ApplicationPaths } from "../../../components/api-authorization/ApiAuthorizationConstants";

function HomePage() {
  // Select authentication state directly from Redux store
  const isAuthenticated = useSelector((state) => state.user.isAuthenticated);
  const username = useSelector((state) => state.user.username);

  if (!isAuthenticated) return <Navigate to={ApplicationPaths.Login} />;
  return (
    <div className="container my-5">
      <div className="p-5 text-center bg-light rounded-3 mb-5">
        <h1 className="mb-3">Welcome to Your Integrated App!</h1>
        <p className="lead">
          This is a sample home page demonstrating Bootstrap 5 layout and
          styling within a React component.
        </p>
        <hr className="my-4" />
        {isAuthenticated ? (
          <>
            <p className="mb-3">Hello, {username}! You are logged in.</p>{" "}
            {/* Use username from Redux */}
            <Link to="/dashboard" className="btn btn-primary btn-lg">
              Go to Dashboard
            </Link>
          </>
        ) : (
          <>
            <p className="mb-3">
              Please log in or register to access full features.
            </p>
            <Link to="/login" className="btn btn-primary btn-lg me-2">
              Login
            </Link>
            <Link to="/register" className="btn btn-outline-secondary btn-lg">
              Register
            </Link>
          </>
        )}
      </div>
      <div className="row g-4">
        {/* Card 1 */}
        <div className="col-md-4">
          <div className="card shadow-sm h-100">
            <div className="card-body">
              <h5 className="card-title">Responsive Design</h5>
              <h6 className="card-subtitle mb-2 text-muted">
                Mobile-first approach
              </h6>
              <p className="card-text">
                Bootstrap 5 is built with a mobile-first philosophy, ensuring
                your app looks great on any device.
              </p>
              <a
                href="https://getbootstrap.com/docs/5.3/layout/breakpoints/"
                className="card-link"
                target="_blank"
                rel="noopener noreferrer"
              >
                Learn More
              </a>
            </div>
          </div>
        </div>

        {/* Card 2 */}
        <div className="col-md-4">
          <div className="card shadow-sm h-100">
            <div className="card-body">
              <h5 className="card-title">Component-Based UI</h5>
              <h6 className="card-subtitle mb-2 text-muted">
                React & Bootstrap harmony
              </h6>
              <p className="card-text">
                Combine the power of React's component architecture with
                Bootstrap's pre-built UI components.
              </p>
              <a
                href="https://react-bootstrap.netlify.app/"
                className="card-link"
                target="_blank"
                rel="noopener noreferrer"
              >
                React-Bootstrap
              </a>
            </div>
          </div>
        </div>

        {/* Card 3 */}
        <div className="col-md-4">
          <div className="card shadow-sm h-100">
            <div className="card-body">
              <h5 className="card-title">Backend Integration</h5>
              <h6 className="card-subtitle mb-2 text-muted">
                ASP.NET Core & APIs
              </h6>
              <p className="card-text">
                Seamlessly connect your React frontend to a robust ASP.NET Core
                API backend.
              </p>
              <Link to="/products" className="card-link">
                View Products API
              </Link>
            </div>
          </div>
        </div>
      </div>{" "}
      {/* End row */}
      <div className="text-center my-5">
        <div className="alert alert-info" role="alert">
          Explore the navigation bar for more features!
        </div>
        <p className="text-muted small">
          &copy; {new Date().getFullYear()} Your App Name. All rights reserved.
        </p>
      </div>
    </div>
  );
}

export default HomePage;
