import React, { useEffect } from 'react';
import { Button, Form, Input, Divider, message } from 'antd';
import { UserOutlined, MailOutlined, KeyOutlined } from '@ant-design/icons';
import { Link, Navigate } from 'react-router-dom';
import { useAppSelector, useAppDispatch } from '../../app/hooks';
import { selectStatus, selectError, resetStatus, registerAsync } from '../../features/user/userSlice';

const Register = () => {
   const status = useAppSelector(selectStatus);
   const error = useAppSelector(selectError);

   const dispatch = useAppDispatch();

   const onFinish = (values: IRegisterRequest) => {
      dispatch(registerAsync(values));
   };

   useEffect(() => {
      if (status === 'error') {
         message.error(error);
         dispatch(resetStatus());
      }
   }, [status]);

   if (status === 'success') {
      dispatch(resetStatus());
      return <Navigate to="/login" />;
   }

   return (
      <div className="place-middle">
         <h1 style={{ textAlign: 'center' }}>Register</h1>
         <Form onFinish={onFinish} style={{ minWidth: '320px' }}>
            <Divider orientation="left" plain>
               Personal information
            </Divider>
            <Form.Item name="firstName" rules={[{ required: true, message: 'Please input your first name!' }]}>
               <Input placeholder="First name" prefix={<UserOutlined />} />
            </Form.Item>
            <Form.Item name="lastName" rules={[{ required: true, message: 'Please input your last name!' }]}>
               <Input placeholder="Last name" prefix={<UserOutlined />} />
            </Form.Item>
            <Form.Item
               name="age"
               rules={[
                  {
                     required: true,
                     message: 'Please input your age!',
                  },
               ]}
            >
               <Input type="number" placeholder="Age" prefix={<UserOutlined />} />
            </Form.Item>
            <Divider orientation="left" plain>
               Privacy information
            </Divider>
            <Form.Item name="email" rules={[{ required: true, message: 'Please input your email!' }]}>
               <Input placeholder="Email" prefix={<MailOutlined />} />
            </Form.Item>
            <Form.Item
               name="password"
               rules={[
                  {
                     required: true,
                     message: 'Please input your password!',
                  },
               ]}
            >
               <Input.Password placeholder="Password" prefix={<KeyOutlined />} />
            </Form.Item>
            <Form.Item
               name="confirmPassword"
               rules={[
                  {
                     required: true,
                     message: 'Please input your password again!',
                  },
               ]}
            >
               <Input.Password placeholder="Confirm password" prefix={<KeyOutlined />} />
            </Form.Item>
            <Form.Item>
               <Button type="primary" htmlType="submit">
                  Sign Up
               </Button>
               <Link style={{ float: 'right', lineHeight: '32px' }} to="/login">
                  Already has an account ?
               </Link>
            </Form.Item>
         </Form>
      </div>
   );
};

export default Register;