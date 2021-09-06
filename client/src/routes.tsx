import HomePage from './pages/Home';
import LoginPage from './pages/auth/login';
import ForgotPage from './pages/auth/forgot';
import RegisterPage from './pages/auth/register';
import CatalogPage from './pages/catalog/index';
import ConfirmPage from './pages/auth/confirm';
import ProfilePage from './pages/profile/index';
import SettingsPage from './pages/profile/settings';
import NotFoundPage from './pages/NotFound';
import AdminPage from './pages/admin/index';
import CoursePage from './pages/Course';
import React from 'react';
import { Route, Routes } from 'react-router-dom';

export default () => {
   return (
      <Routes>
         <Route path="" element={<HomePage />} />
         {/*Auth routes*/}
         <Route path="login" element={<LoginPage />} />
         <Route path="forgot" element={<ForgotPage />} />
         <Route path="register" element={<RegisterPage />} />
         <Route path="confirm" element={<ConfirmPage />} />
         {/*Courses routes*/}
         <Route path="catalog/:page" element={<CatalogPage />} />
         <Route path="course/:id" element={<CoursePage />} />
         {/*Profile routes*/}
         <Route path="profile" element={<ProfilePage />} />
         <Route path="settings" element={<SettingsPage />} />
         {/*Other routes*/}
         <Route path="admin" element={<AdminPage />} />
         <Route path="*" element={<NotFoundPage />} />
      </Routes>
   );
};
