import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from './app/hooks';
import { Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import { Header } from './components/header';
import { Layout } from 'antd';
import { loadUser, logout, selectUser } from './features/identitySlice';
import { LoginPage } from './pages/identity/login';
import { RegisterPage } from './pages/identity/register';
import { CatalogPage } from './pages/catalog/index';
import { DetailsPage } from './pages/details';
import { NotFoundPage } from './pages/404';
import { AdminPage } from './pages/admin/index';
import { ProfilePage } from './pages/profile';
import { ProfileSettingsPage } from './pages/profile/settings';
import 'antd/dist/antd.min.css';
import './App.css';

const { Content } = Layout;

const App = () => {
   const dispatch = useAppDispatch();

   const user = useAppSelector(selectUser);

   const navigate = useNavigate();

   useEffect(() => {
      dispatch(loadUser());
   }, [dispatch]);

   const handleLogout = () => {
      dispatch(logout());
      navigate('/login');
   }

   return (
      <Layout style={{ minHeight: '100vh' }}>
         <Header user={user} onLogout={handleLogout} />
         <Content style={{ padding: '32px 64px' }}>
            <Routes>
               <Route path="login" element={<LoginPage />} />
               <Route path="register" element={<RegisterPage />} />
               <Route path="admin" element={<AdminPage />} />
               <Route path="profile" element={<ProfilePage />} />
               <Route path="profile/settings" element={<ProfileSettingsPage />} />

               <Route path="">
                  <Navigate to="/catalog/1" />
               </Route>

               <Route path="catalog/:page" element={<CatalogPage />} />
               <Route path="details/:id" element={<DetailsPage />} />

               <Route path="*" element={<NotFoundPage />} />
            </Routes>
         </Content>
      </Layout>
   );
};

export default App;
