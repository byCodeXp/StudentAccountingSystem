import React from 'react';
import { Routes, Route } from 'react-router-dom';
import './App.css';
import 'antd/dist/antd.min.css';
import HomePage from './components/Home';
import LoginPage from './components/Login';
import RegisterPage from './components/Register';
import AdminPage from "./components/Pages/Admin";
import EmailConfirm from "./components/Pages/EmailConfirm";
import Catalog from './components/Pages/Catalog';

function App() {
  return (
    <Routes>
      <Route path="" element={<HomePage />} />
      <Route path="login" element={<LoginPage />} />
      <Route path="register" element={<RegisterPage />} />
      <Route path="email" element={<EmailConfirm />} />
      <Route path="catalog" element={<Catalog />} />
      <Route path="admin" element={<AdminPage />} />
    </Routes>
  );
}

export default App;
