import axios from 'axios'
import {AuthModel} from '../models/AuthModel'
import {UserModel} from '../models/UserModel'
import {RegisterUserModel} from '../components/Registration'

const API_URL = process.env.REACT_APP_API_URL || ''

export const GET_USER_BY_ACCESSTOKEN_URL = `${API_URL}User/profile`
export const LOGIN_URL = `${API_URL}account/v2/login`
export const REGISTER_URL = `${API_URL}account/v2/register`

// Server should return AuthModel
export function login(email: string, password: string) {
  return axios.post(LOGIN_URL, {email, password})
}

// Server should return AuthModel
export function register(user: RegisterUserModel) {
  return axios.post<AuthModel>(REGISTER_URL, user)
}

export function getUserByToken() {
  // Authorization head should be fulfilled in interceptor.
  // Check common redux folder => setupAxios
  return axios.get<UserModel>(GET_USER_BY_ACCESSTOKEN_URL)
}
