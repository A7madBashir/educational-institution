// client-app/src/store/userSlice.js (or userSlice.ts)
import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isAuthenticated: false,
  username: null,
  roles: [],
  isLoading: true, // To indicate if auth state is being loaded
  // Add token and refreshToken here if you want them in Redux state,
  // but often they are only in localStorage for security/simplicity.
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    isAuthenticated: (state) => {
      return state.isAuthenticated;
    },
    setUser: (state, action) => {
      state.isAuthenticated = true;
      state.username = action.payload.username;
      state.roles = action.payload.roles || [];
      state.isLoading = false;
    },
    clearUser: (state) => {
      state.isAuthenticated = false;
      state.username = null;
      state.roles = [];
      state.isLoading = false;
    },
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
  },
});

export const { setUser, clearUser, setLoading } = userSlice.actions;
export const userReducer = userSlice.reducer;
