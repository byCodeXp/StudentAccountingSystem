import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from './app/hooks';
import { Route, Routes } from 'react-router-dom';
import { Header } from './components/header';
import { PrivateRoute } from './components/privateRoute';
import { Layout } from 'antd';
import {
   loadUser,
   logout,
   selectStatus,
   selectUser,
} from './features/identitySlice';
import { LoginPage } from './pages/identity/login';
import { RegisterPage } from './pages/identity/register';
import { CatalogPage } from './pages/course/catalog';
import { DetailsPage } from './pages/course/details';
import { NotFoundPage } from './pages/404';
import { AdminPage } from './pages/admin/index';
import 'antd/dist/antd.min.css';
import './App.css';

const { Content } = Layout;

const App = () => {
   const dispatch = useAppDispatch();

   const status = useAppSelector(selectStatus);
   const user = useAppSelector(selectUser);

   useEffect(() => {
      dispatch(loadUser());
   }, [dispatch]);

   const handleLogout = () => {
      dispatch(logout());
   }

   return (
      <Layout style={{ minHeight: '100vh' }}>
         <Header signed={status === 'signed'} user={user} onLogout={handleLogout} />
         <Content style={{ padding: '32px 64px' }}>
            <Routes>
               <PrivateRoute path="login" element={<LoginPage />} condition={status !== 'signed'} redirect="/" />
               <PrivateRoute path="register" element={<RegisterPage />} condition={status !== 'signed'} redirect="/" />
               <PrivateRoute path="/admin" element={<AdminPage />} condition={status === 'signed' && user?.role === 'Admin'} redirect="/login" />

               <Route path="catalog/:page" element={<CatalogPage />} />
               <Route path="details/:id" element={<DetailsPage />} />

               <Route path="*" element={<NotFoundPage />} />
            </Routes>
         </Content>
      </Layout>
   );
};

export default App;
