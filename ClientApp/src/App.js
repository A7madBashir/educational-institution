import React, { Component } from "react";
import { Route, Routes } from "react-router-dom";
import AppRoutes from "./routes/AppRoutes";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import { Layout } from "./components/Layout";
import "./custom.css";

// Import Bootstrap CSS
import 'bootstrap/dist/css/bootstrap.min.css';

// Import Bootstrap JS (includes Popper.js)
// This is crucial for components like dropdowns, modals, etc.
// You might not need this if React components are handling all UI logic
// but it's good to include if you expect browser-managed Bootstrap JS features.
import 'bootstrap/dist/js/bootstrap.bundle.min';


export default class App extends Component {
  static displayName = App.name;
  
  render() {
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, requireAuth, ...rest } = route;
            return (
              <Route
                key={index}
                {...rest}
                element={
                  requireAuth ? (
                    <AuthorizeRoute {...rest} element={element} />
                  ) : (
                    element
                  )
                }
              />
            );
          })}
        </Routes>
      </Layout>
    );
  }
}
