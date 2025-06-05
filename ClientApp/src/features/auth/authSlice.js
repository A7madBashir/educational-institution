import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  status: "idle", // 'idle' | 'loading' | 'succeeded' | 'failed'
  token: null,
  refreshToken: null,
  error: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (state, action) => {
      state.token = action.payload;
      state.status = "succeeded";
      state.error = null;
    },
    logout: (state) => {
      state.status = "idle";
      state.token = null;
      state.error = null;
    },
    loginStart: (state) => {
      state.status = "loading";
      state.error = null;
    },
    loginFailed: (state, action) => {
      state.status = "failed";
      state.error = action.payload;
    },
  },
});

export const { loginSuccess, logout, loginStart, loginFailed } =
  authSlice.actions;

export const authReducer = authSlice.reducer;
