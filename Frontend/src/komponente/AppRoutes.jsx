import React from 'react'
import { Routes, Route } from "react-router-dom"
import O_nama from '../stranice/O_nama'
import Registracija from '../stranice/Registracija'
import Login from '../stranice/Login'
import NotFound from '../stranice/NotFound'
import Dashboard from '../stranice/Dashboard'
import IzvestajZaZubara from '../stranice/IzvestajZaZubara'
import IzvestajZaPacijenta from '../stranice/IzvestajZaPacijenta'

export default function AppRoutes() {
  return (
    <Routes>
      <Route index element={<Login />} />
      <Route path="/o-nama" element={<O_nama />} />
      <Route path="/login" element={<Login />} />
      <Route path="/registracija" element={<Registracija />} />
      <Route path="/dashboard" element={<Dashboard  />} />
      <Route path="/izvestaj-za-zubara" element={<IzvestajZaZubara  />} />
      <Route path="/izvestaj-za-pacijenta" element={<IzvestajZaPacijenta  />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  )
}
