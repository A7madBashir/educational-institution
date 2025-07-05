import React from 'react'
import {BrowserRouter as Router, Route, Routes, Navigate} from 'react-router-dom'
import {PageLink, PageTitle} from '../../../_metronic/layout/core'
import {Feeds} from './components/Feeds'
import {Lists} from './components/Lists'
import {Tables} from './components/Tables'
import {Statistics} from './components/Statistics'

const widgetsBreadCrumbs: Array<PageLink> = [
  {
    title: 'Widgets',
    path: '/crafted/widgets/charts',
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

const WidgetsPage: React.FC<React.PropsWithChildren> = () => {
  return (
    <Routes>
      <Route path='/crafted/widgets/feeds'>
        <PageTitle breadcrumbs={widgetsBreadCrumbs}>Feeds</PageTitle>
        <Feeds />
      </Route>
      <Route path='/crafted/widgets/lists'>
        <PageTitle breadcrumbs={widgetsBreadCrumbs}>Lists</PageTitle>
        <Lists />
      </Route>
      <Route path='/crafted/widgets/tables'>
        <PageTitle breadcrumbs={widgetsBreadCrumbs}>Tables</PageTitle>
        <Tables />
      </Route>
      <Route path='/crafted/widgets/statistics'>
        <PageTitle breadcrumbs={widgetsBreadCrumbs}>Statiscics</PageTitle>
        <Statistics />
      </Route>
      <Navigate to='/crafted/widgets/lists' />
      <Navigate to='/crafted/widgets/lists' />
    </Routes>
  )
}

export default WidgetsPage
