import React from 'react';

import { Layout } from 'antd';
import { Header } from './components/Header';
import 'antd/dist/antd.min.css';
import './styles/app.css';

import { useAppSelector } from './app/hooks';
import { selectUser } from './features/user/userSlice';

import Routes from './routes';

const { Content } = Layout;

function App() {
   const user = useAppSelector(selectUser);

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
