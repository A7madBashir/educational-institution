export interface AuthModel {
  token: string
  refreshToken?: string
  tokenValidTo?: Date
  refreshTokenValidTo?: Date
}
