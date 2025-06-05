import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";

import {
  loginFailed,
  loginStart,
  loginSuccess,
} from "../../../features/auth/authSlice";
import { setUser } from "../../../features/user/userSlice";

import authService from "../../../services/AuthService";

export default function LoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const authStatus = useSelector((state) => state.auth.status);
  const authError = useSelector((state) => state.auth.error);
  const isAuthenticated = useSelector((state) => state.user.isAuthenticated);

  useEffect(() => {
    if (authStatus === "succeeded" && isAuthenticated) {
      setMessage("Login successful!");

      navigate("/");
    } else if (authStatus === "failed") {
      setMessage(authError || "Login failed. Please try again.");
    } else if (authStatus === "idle") {
      setMessage("");
    }
  }, [authStatus, isAuthenticated, authError, navigate]);

  const handleLogin = async (e) => {
    e.preventDefault();
    setMessage("");

    dispatch(loginStart());

    try {
      const resData = await authService.login(email, password);

      if (resData?.accessToken) {
        dispatch(loginSuccess(resData.accessToken));
        dispatch(setUser({ username: resData.username, roles: resData.roles }));
      } else {
        dispatch(loginFailed("Invalid email and password"));
      }
    } catch (error) {
      const errorMessage =
        error?.message || "Invalid email and password.";
      dispatch(loginFailed(errorMessage));
    }
  };

  return (
    <div className="container mt-5">
      {" "}
      {/* margin-top 5 */}
      <div className="row justify-content-center">
        {" "}
        {/* Center content */}
        <div className="col-md-6 col-lg-4">
          {" "}
          {/* Responsive column sizing */}
          <div className="card shadow-sm p-4">
            {" "}
            {/* Card with shadow and padding */}
            <h2 className="card-title text-center mb-4">Login</h2>{" "}
            {/* Centered title */}
            <form onSubmit={handleLogin}>
              <div className="mb-3">
                {" "}
                {/* Margin-bottom for form groups */}
                <label htmlFor="emailInput" className="form-label">
                  Email address
                </label>
                <input
                  type="email"
                  className="form-control"
                  id="emailInput"
                  placeholder="name@example.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              <div className="mb-3">
                <label htmlFor="passwordInput" className="form-label">
                  Password
                </label>
                <input
                  type="password"
                  className="form-control"
                  id="passwordInput"
                  placeholder="Enter your password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
              </div>

              {/* Message Display Area */}
              {authStatus === "loading" && (
                <div className="alert alert-info text-center" role="alert">
                  Processing login...
                </div>
              )}
              {message && (
                <div
                  className={`alert ${authStatus === "succeeded" ? "alert-success" : "alert-danger"} text-center`}
                  role="alert"
                >
                  {message}
                </div>
              )}

              <button
                type="submit"
                className="btn btn-primary w-100 mt-3"
                disabled={authStatus === "loading"}
              >
                {authStatus === "loading" ? "Logging In..." : "Login"}
              </button>
            </form>
            {/* <p className="text-center mt-3 mb-0">
              Don't have an account? <Link to="/register">Register here</Link>
            </p> */}
          </div>
        </div>
      </div>
    </div>
  );
}
