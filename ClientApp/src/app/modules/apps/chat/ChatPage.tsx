import React from 'react'
import {BrowserRouter as Router, Route, Routes, Navigate} from 'react-router-dom'
import {PageLink, PageTitle} from '../../../../_metronic/layout/core'
import {Private} from './components/Private'
import {Group} from './components/Group'
import {Drawer} from './components/Drawer'

const chatBreadCrumbs: Array<PageLink> = [
  {
    title: 'Chat',
    path: '/apps/chat/private-chat',
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

const ChatPage: React.FC<React.PropsWithChildren> = () => {
  return (
    <Routes>
      <Route path='/apps/chat/private-chat'>
        <PageTitle breadcrumbs={chatBreadCrumbs}>Private chat</PageTitle>
        <Private />
      </Route>
      <Route path='/apps/chat/group-chat'>
        <PageTitle breadcrumbs={chatBreadCrumbs}>Group chat</PageTitle>
        <Group />
      </Route>
      <Route path='/apps/chat/drawer-chat'>
        <PageTitle breadcrumbs={chatBreadCrumbs}>Drawer chat</PageTitle>
        <Drawer />
      </Route>
      <Navigate to='/apps/chat/private-chat' />
      <Navigate to='/apps/chat/private-chat' />
    </Routes>
  )
}

export default ChatPage
