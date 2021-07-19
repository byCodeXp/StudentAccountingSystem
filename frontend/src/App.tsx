import React from 'react';
import './App.css';
import 'antd/dist/antd.min.css';
import AppHeader from './Header';
import { Layout, Form, Input, Checkbox, Button } from 'antd';
const { Content, Footer } = Layout;

function App() {
  const onFinish = (values: any) => {
    console.log('Success:', values);
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };
  return (
    <Layout>
      <AppHeader />
      <Content
        style={{
          padding: '25px 50px',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <Form
          name="basic"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={{ remember: true }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          style={{ width: '480px' }}
        >
          <Form.Item
            label="Username"
            name="username"
            rules={[{ required: true, message: 'Please input your username!' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Password"
            name="password"
            rules={[{ required: true, message: 'Please input your password!' }]}
          >
            <Input.Password />
          </Form.Item>

          <Form.Item
            name="remember"
            valuePropName="checked"
            wrapperCol={{ offset: 8, span: 16 }}
          >
            <Checkbox>Remember me</Checkbox>
          </Form.Item>

          <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <Button type="primary" htmlType="submit">
                Submit
              </Button>
              <Button
                type="link"
                href={'https://google.com'}
                style={{ padding: '0' }}
              >
                Create new account
              </Button>
            </div>
          </Form.Item>
        </Form>
      </Content>
      <Footer>Footer</Footer>
    </Layout>
  );
}

export default App;
