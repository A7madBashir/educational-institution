import {Action} from '@reduxjs/toolkit'
import {persistReducer} from 'redux-persist'
import storage from 'redux-persist/lib/storage'
import {put, takeLatest} from 'redux-saga/effects'
import {UserModel} from '../models/UserModel'
import {getUserByToken} from './AuthCRUD'

export interface ActionWithPayload<T> extends Action {
  payload?: T
}

export const actionTypes = {
  Login: '[Login] Action',
  Logout: '[Logout] Action',
  Register: '[Register] Action',
  UserRequested: '[Request User] Action',
  UserLoaded: '[Load User] Auth API',
  SetUser: '[Set User] Action',
}

const initialAuthState: IAuthState = {
  user: undefined,
  token: undefined,
  refreshToken: undefined,
  refreshTokenValidTo: undefined,
  tokenValidTo: undefined,
}

export interface IAuthState {
  user?: UserModel
  token?: string
  refreshToken?: string
  tokenValidTo?: Date
  refreshTokenValidTo?: Date
}

export const reducer = persistReducer(
  {
    storage,
    key: 'auth-model',
    whitelist: ['user', 'token', 'refreshToken', 'tokenValidTo', 'refreshTokenValidTo'],
  },
  (state: IAuthState = initialAuthState, action: ActionWithPayload<IAuthState>) => {
    switch (action.type) {
      case actionTypes.Login: {
        return {...action.payload, user: undefined}
      }

      case actionTypes.Register: {
        return {...action.payload, user: undefined}
      }

      case actionTypes.Logout: {
        return initialAuthState
      }

      case actionTypes.UserRequested: {
        return {...state, user: undefined}
      }

      case actionTypes.UserLoaded: {
        const user = action.payload?.user
        return {...state, user}
      }

      case actionTypes.SetUser: {
        const user = action.payload?.user
        return {...state, user}
      }

      default:
        return state
    }
  }
)

export const actions = {
  login: (payload: IAuthState) => ({type: actionTypes.Login, payload: payload}),
  register: (payload: IAuthState) => ({
    type: actionTypes.Register,
    payload: payload,
  }),
  logout: () => ({type: actionTypes.Logout}),
  requestUser: () => ({
    type: actionTypes.UserRequested,
  }),
  fulfillUser: (user: UserModel) => ({type: actionTypes.UserLoaded, payload: {user}}),
  setUser: (user: UserModel) => ({type: actionTypes.SetUser, payload: {user}}),
}

export function* saga() {
  yield takeLatest(actionTypes.Login, function* loginSaga() {
    yield put(actions.requestUser())
  })

  yield takeLatest(actionTypes.Register, function* registerSaga() {
    yield put(actions.requestUser())
  })

  yield takeLatest(actionTypes.UserRequested, function* userRequested() {
    const {data: user} = yield getUserByToken()
    yield put(actions.fulfillUser(user))
  })
}
