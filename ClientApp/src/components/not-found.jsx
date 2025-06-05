import { Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css"; // Ensure Bootstrap CSS is available if this component uses it

function NotFoundPage() {
  return (
    <div
      className="container d-flex flex-column justify-content-center align-items-center"
      style={{ minHeight: "calc(100vh - 100px)" }}
    >
      {" "}
      {/* Adjust minHeight as needed */}
      <h1 className="display-1 text-danger mb-4">404</h1>
      <h2 className="mb-3">Page Not Found</h2>
      <p className="lead text-center mb-4">
        Oops! The page you are looking for might have been removed, had its name
        changed, or is temporarily unavailable.
      </p>
      <Link to="/" className="btn btn-primary btn-lg">
        Go to Home Page
      </Link>
    </div>
  );
}

export default NotFoundPage;
