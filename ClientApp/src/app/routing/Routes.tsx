/**
 * High level router.
 *
 * Note: It's recommended to compose related routes in internal router
 * components (e.g: `src/app/modules/Auth/pages/AuthPage`, `src/app/BasePage`).
 */

import React, {FC} from 'react'
import {Routes as AppRoutes, Navigate, Route} from 'react-router-dom'
import {shallowEqual, useSelector} from 'react-redux'
import {MasterLayout} from '../../_metronic/layout/MasterLayout'
import {PrivateRoutes} from './PrivateRoutes'
import {Logout, AuthPage} from '../modules/auth'
import {ErrorsPage} from '../modules/errors/ErrorsPage'
import {RootState} from '../../setup'

const Routes: FC = () => {
  const isAuthorized = useSelector<RootState>(({auth}) => auth.user, shallowEqual)

  return (
    <AppRoutes>
      {/* Public Routes (accessible to everyone) */}
      <Route path='/error/*' element={<ErrorsPage />} />
      <Route path='/logout' element={<Logout />} />

      {!isAuthorized && <Route path='/auth/*' element={<AuthPage />} />}

      {isAuthorized ? (
        <Route
          path='/*'
          element={
            <MasterLayout>
              <PrivateRoutes />
            </MasterLayout>
          }
        />
      ) : (
        <Route path='*' element={<Navigate to='/auth/login' replace />} />
      )}
    </AppRoutes>
  )
}

export {Routes}
