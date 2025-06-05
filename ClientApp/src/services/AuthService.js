import axios from "axios"; // We'll use axios for API calls
import { jwtDecode } from "jwt-decode"; // For decoding JWTs (npm install jwt-decode)
import RequestHandler from "../handler/requestHandler";
import { useReducer } from "react";
import { store } from "../redux/store";
import { logout } from "../features/auth/authSlice";
import { clearUser } from "../features/user/userSlice";

// --- Configuration ---
// Get your API base URL from environment variables (e.g., REACT_APP_API_BASE_URL)

const TOKEN_KEY = "bearer_token"; // Key for storing JWT in localStorage
class AuthService {
  constructor() {
    // This is typically done via Axios interceptors rather than here for every request.
    // However, for explicit manual requests, you could add headers here.
  }

  /**
   * Performs the login API call with email and password.
   * On success, stores the JWT and decodes user information.
   * @param {string} email - User's email.
   * @param {string} password - User's password.
   * @returns {Promise<Object>} - Decoded user data (username, roles, etc.) on success.
   * @throws {Error} - If login fails (API error or network error).
   */
  async login(email, password) {
    try {
      const response = await RequestHandler(`/account/login`, "POST", {
        body: {
          email,
          password,
        },
      });

      console.log(response);
      if (response.status !== 200) throw new Error("UnAuthorized");

      const {
        tokenType,
        expiresIn,
        accessToken: token,
        refreshToken,
      } = response.body; // Assuming your API returns token, username, roles

      // const userData = {
      //   username: username, // Use username from API response for consistency
      //   roles: roles, // Use roles from API response
      //   userId: decodedToken.sub, // The 'sub' (subject) claim is usually the user ID
      //   email: decodedToken.email, // The 'email' claim
      //   // Add any other claims you've put in your JWT and want to expose
      // };

      return response.body; // Return the user data to update AuthContext
    } catch (error) {
      throw error; // Re-throw to be handled by the calling component (e.g., Login.js)
    }
  }

  /**
   * Logs out the user by removing the JWT from localStorage.
   * (If using cookie-based auth, you'd make an API call to clear the cookie server-side).
   */
  logout() {
    store.dispatch(logout());
    store.dispatch(clearUser());
    // If you had a backend logout endpoint for cookie-based auth:
    // try { await axios.post(`${API_BASE_URL}/Auth/logout`); } catch (err) { console.error("Logout failed:", err); }
  }

  /**
   * Checks if a user is currently authenticated by verifying the presence and validity of a JWT.
   * @returns {boolean} - True if authenticated, false otherwise.
   */
  isAuthenticated() {
    return store.getState((state) => state).user.isAuthenticated || false;
  }

  /**
   * Gets the currently stored JWT.
   * @returns {string|null} - The JWT string or null if not found.
   */
  getToken() {
    return store.getState((state) => state.auth.token);
  }

  /**
   * Gets user data from the stored JWT.
   * Call this only if isAuthenticated() returns true.
   * @returns {Object|null} - Decoded user data or null.
   */
  getUserData() {
    const token = this.getToken();
    if (token) {
      try {
        // fetch user by token
        // return user
      } catch (error) {
        console.error(
          "AuthService: Failed to get user data from token.",
          error
        );
        return null;
      }
    }
    return null;
  }

  /**
   * (Optional) Sets up an Axios interceptor to automatically attach the JWT to requests.
   * Call this once when your app starts (e.g., in App.js or index.js).
   */
  setupAxiosInterceptor() {
    axios.interceptors.request.use(
      (config) => {
        const token = this.getToken();
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // Optional: Response interceptor to handle 401/403 globally
    axios.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response && error.response.status === 401) {
          // If 401, token might be expired or invalid. Logout the user.
          console.warn("AuthService: Received 401 Unauthorized. Logging out.");
          this.logout();
          // You might dispatch an action to redirect to login or show a message
          // This would typically involve a callback provided by AuthContext/Redux
        }
        if (error.response && error.response.status === 403) {
          // If 403, authenticated but forbidden.
          console.warn(
            "AuthService: Received 403 Forbidden. User lacks permission."
          );
          // You might dispatch an action to show an "Access Denied" page/message
        }
        return Promise.reject(error);
      }
    );
  }
}

// Export a singleton instance of the service
const authService = new AuthService();
export default authService;
