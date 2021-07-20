import { Button, Row, Col, Form, Input } from 'antd';
import { Link } from 'react-router-dom';
import { KeyOutlined, UserOutlined, MailOutlined } from '@ant-design/icons';

function RegisterPage() {
  return (
    <Row>
      <Col
        xs={{ span: 20, offset: 2 }}
        sm={{ span: 16, offset: 4 }}
        md={{ span: 10, offset: 7 }}
        lg={{ span: 8, offset: 8 }}
        xl={{ span: 6, offset: 9 }}
        xxl={{ span: 4, offset: 10 }}
        style={{ marginTop: '50vh', transform: 'translateY(-100%)' }}
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
  );
}

export default RegisterPage;
