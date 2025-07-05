import {configureStore} from '@reduxjs/toolkit'
import createSagaMiddleware from 'redux-saga'
import {reduxBatch} from '@manaflair/redux-batch'
import {persistStore} from 'redux-persist'
import {rootReducer, rootSaga} from './RootReducer'

const sagaMiddleware = createSagaMiddleware()
const middleware = [sagaMiddleware]

const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware: any) =>
    getDefaultMiddleware({
      immutableCheck: false,
      serializableCheck: false,
      thunk: true,
    }).concat(middleware),
  devTools: process.env.NODE_ENV !== 'production',
  enhancers: (getDefaultEnhancers: any) => getDefaultEnhancers().concat(reduxBatch),
})

export type AppDispatch = typeof store.dispatch

/**
 * @see https://github.com/rt2zz/redux-persist#persiststorestore-config-callback
 * @see https://github.com/rt2zz/redux-persist#persistor-object
 */
export const persistor = persistStore(store)

sagaMiddleware.run(rootSaga)

export default store
export type AppStore = typeof store
