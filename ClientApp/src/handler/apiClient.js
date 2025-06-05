// client-app/src/utils/apiClient.js
import RequestHandler from "./RequestHandler"; // Your updated RequestHandler
import authService from "../services/AuthService"; // Your AuthService

/**
 * A wrapper around RequestHandler that automatically includes the JWT.
 * It also handles global 401/403 responses by logging out.
 *
 * @param {string} endpoint - The specific API path relative to API_BASE_URL.
 * @param {string} method - The HTTP method.
 * @param {object} options - Optional configuration object.
 */
export async function apiClient(endpoint, method, options = {}) {
  const token = authService.getToken(); // Get the token

  // Prepare headers, ensuring Authorization header is added if token exists
  const headers = { ...options.headers };
  if (token) {
    headers.Authorization = `Bearer ${token}`;
  }

  try {
    const response = await RequestHandler(endpoint, method, {
      ...options,
      headers: headers, // Pass the merged headers
    });
    return response;
  } catch (error) {
    // Global handling for 401 Unauthorized
    if (error.status === 401) {
      console.warn(
        "API Client: Received 401 Unauthorized. Session expired or invalid token. Logging out."
      );
      authService.logout(); // Clear token from storage
      // TODO: Here, you might want to trigger a redirect to login.
      // This often requires access to `Maps` from react-router-dom,
      // which can be tricky in a plain JS file. Consider:
      // A) Using a global event emitter.
      // B) Using a state management solution (Redux) to dispatch a logout/redirect action.
      // C) Redirecting directly: window.location.href = '/login'; (Less ideal, no React Router context)
    }
    // Global handling for 403 Forbidden
    else if (error.status === 403) {
      console.warn(
        "API Client: Received 403 Forbidden. User lacks permission."
      );
      // TODO: Handle this, e.g., redirect to an access denied page or show a message.
      // navigate('/access-denied');
    }
    throw error; // Re-throw the structured error for component-specific handling
  }
}

// Optional: Export specific methods for convenience
export const apiGet = (endpoint, options) =>
  apiClient(endpoint, "GET", options);
export const apiPost = (endpoint, options) =>
  apiClient(endpoint, "POST", options);
export const apiPut = (endpoint, options) =>
  apiClient(endpoint, "PUT", options);
export const apiDelete = (endpoint, options) =>
  apiClient(endpoint, "DELETE", options);
