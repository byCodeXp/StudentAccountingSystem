import { Button, Row, Col, Form, Input } from 'antd';
import { Link } from 'react-router-dom';
import { KeyOutlined, UserOutlined, MailOutlined } from '@ant-design/icons';
import Page from './Page';

function RegisterPage() {
  return (
    <Page>
      <Row style={{ position: 'fixed', width: '100%', height: '100%', left: 0, top: 0 }}>
        <Col
          xs={{ span: 20 }}
          sm={{ span: 16 }}
          md={{ span: 10 }}
          lg={{ span: 8 }}
          xl={{ span: 6 }}
          xxl={{ span: 4 }}
          style={{ margin: 'auto' }}
        >
          <h1 style={{ textAlign: 'center' }}>Register</h1>
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
              name="email"
              rules={[{ required: true, message: 'Please input your email!' }]}
            >
              <Input type="email" placeholder="Email" prefix={<MailOutlined />} />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input.Password placeholder="Password" prefix={<KeyOutlined />} />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                Sign Up
              </Button>
              <Link style={{ float: 'right', lineHeight: '32px' }} to="/login">
                Already has account ?
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

export default RegisterPage;
