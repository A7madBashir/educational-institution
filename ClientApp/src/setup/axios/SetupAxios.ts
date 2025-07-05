import {AppStore} from '../redux/Store'

export default function setupAxios(axios: any, store: AppStore) {
  axios.interceptors.request.use(
    (config: any) => {
      const {token} = store.getState().auth

      if (token) {
        config.headers.Authorization = `Bearer ${token}`
      }

      return config
    },
    (err: any) => Promise.reject(err)
  )
}
