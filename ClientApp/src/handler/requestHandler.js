import axios from "axios";

const API_BASE_URL = "https://localhost:7133"; // environment variable endpoint
// const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'https://localhost:7001/api';

/**
 * Handles API requests with a consistent configuration.
 *
 * @param {string} endpoint - The specific API path relative to API_BASE_URL (e.g., "/Auth/login").
 * @param {string} method - The HTTP method (e.g., "GET", "POST", "PUT", "DELETE").
 * @param {object} options - Optional configuration object.
 * @param {object} [options.headers] - Custom headers to merge.
 * @param {object} [options.body] - The request body for POST/PUT/PATCH methods (will be JSON.stringified automatically by Axios).
 * @param {object} [options.query] - Query parameters for GET requests.
 */
export default async function RequestHandler(endpoint, method, options = {}) {
  // Construct the full request URL
  const fullUrl = `${API_BASE_URL}${endpoint}`;

  // Configure the Axios request object
  const requestConfig = {
    url: fullUrl,
    method: method,
    timeout: 60000, // Request timeout in milliseconds
    headers: {
      "Content-Type": "application/json", // Default to JSON content type
      ...options.headers, // Merge any custom headers provided in options
    },
    data: options.body,
    params: options.query, // For query parameters in the URL
    withCredentials: true, // Important for sending cookies/authentication headers across origins
  };

  try {
    const result = await axios.request(requestConfig); // Use axios.request with the config object

    return {
      status: result.status,
      ok: result.statusText, // This will be "OK" for 200, "Created" for 201, etc.
      body: result.data,
    };
  } catch (error) {
    // Axios throws an error for non-2xx status codes
    console.error(`Request Failed: ${method} ${fullUrl}`, error);

    // Re-throw with more structured error information
    throw {
      status: error.response ? error.response.status : null,
      message: error.response?.data?.message || error.message,
      details: error.response?.data?.errors || null, // For validation errors (from ErrorModel)
      isNetworkError: !error.response, // True if it's a network/timeout error, not an HTTP response
      originalError: error, // Keep the original error for debugging
    };
  }
}
