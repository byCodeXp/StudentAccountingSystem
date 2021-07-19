import React from 'react';
import { Button, Layout } from "antd";
import { Link } from "react-router-dom";

const { Header, Content, Footer } = Layout;

function Page() {
  return (
    <Layout>
      <Header style={{ display: 'flex', justifyContent: 'space-between' }}>
        <Link to="">
          <span style={{ color: 'white' }}>BrandName</span>
        </Link>
        <div>
          <Link to="login">
            <Button type="link">Login</Button>
          </Link>
          <span style={{ color: 'white' }}>|</span>
          <Link to="register">
            <Button type="link">Register</Button>
          </Link>
        </div>
      </Header>
      <Content>Content</Content>
      <Footer>Footer</Footer>
    </Layout>
  );
}

export default Page;
