import React from 'react';
import { Routes, Route, Link } from 'react-router-dom';
import './App.css';
import 'antd/dist/antd.min.css';
import { Button, Layout } from 'antd';
import HomePage from './components/Home';
import LoginPage from './components/Login';
import RegisterPage from './components/Register';

const { Header, Content, Footer } = Layout;

function App() {
  return (
    <Layout>
      <Header style={{ display: 'flex', justifyContent: 'space-between' }}>
        <Link to="">
          <span style={{ color: 'white' }}>BrandName</span>
        </Link>
        <Link to="login">Sign In</Link>
      </Header>
      <Content style={{ padding: '25px 50px' }}>
        <Routes>
          <Route path="" element={<HomePage />} />
          <Route path="login" element={<LoginPage />} />
          <Route path="register" element={<RegisterPage />} />
        </Routes>
      </Content>
      <Footer>Footer</Footer>
    </Layout>
  );
}

export default App;
