import React from 'react'
import {BrowserRouter as Router, Route, Routes, Navigate} from 'react-router-dom'
import {PageLink, PageTitle} from '../../../_metronic/layout/core'
import {Overview} from './components/Overview'
import {Projects} from './components/Projects'
import {Campaigns} from './components/Campaigns'
import {Documents} from './components/Documents'
import {Connections} from './components/Connections'
import {ProfileHeader} from './ProfileHeader'

const profileBreadCrumbs: Array<PageLink> = [
  {
    title: 'Profile',
    path: '/profile/overview',
    isSeparator: false,
    isActive: false,
  },
  {
    title: '',
    path: '',
    isSeparator: true,
    isActive: false,
  },
]

const ProfilePage: React.FC<React.PropsWithChildren> = () => {
  return (
    <>
      <ProfileHeader />
      <Routes>
        <Route
          path='/profile/overview'
          element={
            <>
              <Overview />
            </>
          }
        />
        <Route
          path='/profile/projects'
          element={
            <>
              <PageTitle breadcrumbs={profileBreadCrumbs}>Projects</PageTitle>
              <Projects />
            </>
          }
        />
        <Route
          path='/profile/campaigns'
          element={
            <>
              <PageTitle breadcrumbs={profileBreadCrumbs}>Campaigns</PageTitle>
              <Campaigns />
            </>
          }
        />
        <Route
          path='/profile/documents'
          element={
            <>
              <PageTitle breadcrumbs={profileBreadCrumbs}>Documents</PageTitle>
              <Documents />
            </>
          }
        />
        <Route
          path='/profile/connections'
          element={
            <>
              <PageTitle breadcrumbs={profileBreadCrumbs}>Connections</PageTitle>
              <Connections />
            </>
          }
        />
        <Route path='' element={<Navigate to='/profile/overview' replace />} />
      </Routes>
    </>
  )
}

export default ProfilePage
