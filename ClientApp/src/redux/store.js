import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { authReducer } from "../features/auth/authSlice";
import storage from "redux-persist/lib/storage"; // defaults to localStorage for web
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import { userReducer } from "../features/user/userSlice";

// 1. Configuration for Redux Persist
const persistConfig = {
  key: "root", // The key for the localStorage object
  storage, // The storage medium (localStorage)
};

// 2. Combine your reducers
const rootReducer = combineReducers({
  user: userReducer,
  auth: authReducer,
  // ... other reducers
});

// 3. Create a persisted reducer
const persistedReducer = persistReducer(persistConfig, rootReducer);

// 4. Configure the store with the persisted reducer
export const store = configureStore({
  reducer: persistedReducer, // Use the persisted reducer here
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      // Exclude redux-persist actions from serializable check
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
  // devTools: process.env.NODE_ENV !== 'production', // configureStore sets this by default
});

// 5. Create a persistor object
export const persistor = persistStore(store);
