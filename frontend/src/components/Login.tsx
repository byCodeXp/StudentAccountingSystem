import { Form, Input, Checkbox, Button, Col, Row } from 'antd';
import { Link } from 'react-router-dom';
import { UserOutlined, KeyOutlined } from '@ant-design/icons';
import Page from './Page';

function LoginPage() {
  return (
    <Page>
      <Row style={{ position: 'fixed', width: '100%', height: '100%', left: 0, top: 0 }}>
        <Col
          xs={{ span: 20, offset: 2 }}
          sm={{ span: 16, offset: 4 }}
          md={{ span: 10, offset: 7 }}
          lg={{ span: 8, offset: 8 }}
          xl={{ span: 6, offset: 9 }}
          xxl={{ span: 4, offset: 10 }}
          style={{ margin: 'auto' }}
        >
          <h1 style={{ textAlign: 'center' }}>Login</h1>
          <Form
            name="basic"
            initialValues={{ remember: true }}
            labelCol={{ span: 7 }}
          >
            <Form.Item
              name="username"
              rules={[{ required: true, message: 'Please input your username!' }]}
            >
              <Input placeholder="Username" prefix={<UserOutlined />} />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input placeholder="Password" prefix={<KeyOutlined />} />
            </Form.Item>
            <Form.Item name="remember" valuePropName="checked">
              <Checkbox>Remember me</Checkbox>
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                Sign In
              </Button>
              <Link style={{ float: 'right', lineHeight: '32px' }} to="/register">
                Create new account
              </Link>
            </Form.Item>
          </Form>
          <div
            style={{
              textAlign: 'center',
              marginBottom: '12px',
              marginTop: '-12px',
            }}
          >
            or
          </div>
          <Link to="">
            <Button type="primary" style={{ width: '100%' }}>
              Continue with facebook
            </Button>
          </Link>
        </Col>
      </Row>
    </Page>
  );
}

export default LoginPage;
