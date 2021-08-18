import 'antd/dist/antd.min.css';
import './app.css';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from 'antd';
import Header from './components/Header';
import HomePage from './pages/Home';
import LoginPage from './pages/Login';
import ForgotPage from './pages/Forgot';
import RegisterPage from './pages/Register';
import CatalogPage from './pages/Catalog';
import ConfirmPage from './pages/Confirm';
import ProfilePage from './pages/Profile';
import SettingsPage from './pages/Settings';
import NotFoundPage from './pages/NotFound';
import { useAppSelector } from './app/hooks';
import { selectUser } from './features/user/userSlice';

const { Content } = Layout;

function App() {
   const user = useAppSelector(selectUser);

   return (
      <Layout style={{ minHeight: '100vh' }}>
         <Header user={user} />
         <Content style={{ padding: '32px 64px' }}>
            <Routes>
               <Route path="" element={<HomePage />} />
               <Route path="login" element={<LoginPage />} />
               <Route path="forgot" element={<ForgotPage />} />
               <Route path="register" element={<RegisterPage />} />
               <Route path="confirm" element={<ConfirmPage />} />
               <Route path="catalog" element={<CatalogPage />} />
               <Route path="profile" element={<ProfilePage />} />
               <Route path="settings" element={<SettingsPage />} />
               <Route path="*" element={<NotFoundPage />} />
            </Routes>
         </Content>
      </Layout>
   );
}

export default App;
