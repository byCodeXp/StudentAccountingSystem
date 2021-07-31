import React from 'react';
import { Layout } from "antd";
import Header from "./Header";

const { Content, Footer } = Layout;

interface Props {
  children: React.ReactNode;
}

export default ({ children } : Props) => {
  return (
    <Layout>
      <Header />
      <Content style={{ padding: '32px 48px' }}>
        {children}
      </Content>
      <Footer />
    </Layout>
  );
};