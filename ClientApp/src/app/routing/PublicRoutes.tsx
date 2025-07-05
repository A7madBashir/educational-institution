import {BrowserRouter as Router, Route, Routes, Navigate} from 'react-router-dom'
import {AuthPage} from '../modules/auth'

export function PublicRoutes() {
  return (
    <Routes>
      <Route path='/auth' Component={AuthPage} />
      <Route path='*' element={<Navigate to='/auth' replace />} />
    </Routes>
  )
}
