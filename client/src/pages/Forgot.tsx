import React from 'react';
import { Button, Form, Input, Row } from 'antd';
import { MailOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';

const Forgot = () => {
   return (
      <div className="place-middle">
         <h1 style={{ textAlign: 'center' }}>Reset password</h1>
         <Form style={{ minWidth: '320px' }}>
            <Form.Item
               name="email"
               rules={[{ required: true, message: 'Please input your email!' }]}
            >
               <Input placeholder="Email" prefix={<MailOutlined />} />
            </Form.Item>
            <Form.Item name="remember" valuePropName="checked">
               <Row justify="space-between" align="middle">
                  <Link to="/login">Back to login</Link>
                  <Button type="primary" htmlType="submit">
                     Send
                  </Button>
               </Row>
            </Form.Item>
         </Form>
      </div>
   );
};

export default Forgot;
