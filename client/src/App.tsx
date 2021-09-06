import React, { useEffect } from 'react';

import { Layout } from 'antd';
import { Header } from './components/Header';
import 'antd/dist/antd.min.css';
import './styles/app.css';

import { useAppDispatch, useAppSelector } from './app/hooks';
import { loadUser, selectUser } from './features/identitySlice';

import Routes from './routes';

const { Content } = Layout;

function App() {
   const dispatch = useAppDispatch();
   const user = useAppSelector(selectUser);

   useEffect(() => {
      dispatch(loadUser());
   }, []);

   return (
      <Layout style={{ minHeight: '100vh' }}>
         <Header user={user} />
         <Content style={{ padding: '32px 64px' }}>
            <Routes />
         </Content>
      </Layout>
   );
}

export default App;
