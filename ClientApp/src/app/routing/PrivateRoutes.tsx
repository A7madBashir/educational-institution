import React, {Suspense, lazy} from 'react'
import {BrowserRouter as Router, Route, Routes, Navigate} from 'react-router-dom'
import {FallbackView} from '../../_metronic/partials'
import {DashboardWrapper} from '../pages/dashboard/DashboardWrapper'
import {MenuTestPage} from '../pages/MenuTestPage'
const ProfilePage = lazy(() => import('../modules/profile/ProfilePage'))
const AccountPage = lazy(() => import('../modules/accounts/AccountPage'))

export function PrivateRoutes() {

  return (
    <Suspense fallback={<FallbackView />}>
      <Routes>
        <Route path='/' element={<DashboardWrapper />} />
        <Route path='/dashboard/*' element={<DashboardWrapper />} />
        {/* <Route path='/builder' element={<BuilderPageWrapper />} /> */}
        <Route path='/profile/*' element={<ProfilePage />} />
        {/* <Route path='/crafted/pages/wizards' element={<WizardsPage />} />
        <Route path='/crafted/widgets' element={<WidgetsPage />} /> */}
        <Route path='/account/*' element={<AccountPage />} />
        {/* <Route path='/apps/chat' element={<ChatPage />} /> */}
        {/* <Route path='/menu-test' element={<MenuTestPage />} /> */}
        <Route path='*' element={<Navigate to='error/404' replace />} />
      </Routes>
    </Suspense>
  )
}
