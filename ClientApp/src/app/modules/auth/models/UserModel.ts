import {AuthModel} from './AuthModel'

export interface UserModel {
  id: string
  userName: string
  email: string
  firstName: string
  lastName: string
  name: string
  job: string
  lastLoginAt: Date
  dateOfBirth: Date
  gender: string
  nationality: string
  phoneNumber?: string
  roles?: Array<string>
  auth?: AuthModel
}
