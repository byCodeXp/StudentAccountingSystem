import { useEffect } from 'react';
import { Link, Navigate } from 'react-router-dom';
import { Button, Checkbox, Form, Input, Row, message } from 'antd';
import { KeyOutlined, MailOutlined } from '@ant-design/icons';
import { useAppSelector, useAppDispatch } from '../../app/hooks';
import { selectStatus, selectError, loginAsync, resetStatus } from '../../features/identitySlice';

const LoginPage = () => {
   const dispatch = useAppDispatch();

   const status = useAppSelector(selectStatus);
   const errorMessage = useAppSelector(selectError);

   const onFinish = (values: ILoginRequest) => {
      dispatch(loginAsync(values));
   };

   useEffect(() => {
      if (status === 'failed') {
         message.error(errorMessage);
         dispatch(resetStatus());
      }
   }, [status]);

   if (status === 'signed') {
      return <Navigate to="/catalog/1" />;
   }

   return (
      <div className="place-middle">
         <h1 style={{ textAlign: 'center' }}>Login</h1>
         <Form onFinish={onFinish} style={{ minWidth: '320px' }}>
            <Form.Item
               name="email"
               rules={[{ required: true, message: 'Please input your email!' }]}
            >
               <Input
                  disabled={status === 'loading'}
                  placeholder="Email"
                  prefix={<MailOutlined />}
                  autoComplete="username"
               />
            </Form.Item>
            <Form.Item
               name="password"
               rules={[{ required: true, message: 'Please input your password!' }]}
            >
               <Input.Password
                  disabled={status === 'loading'}
                  placeholder="Password"
                  prefix={<KeyOutlined />}
                  autoComplete="current-password"
               />
            </Form.Item>
            <Form.Item name="remember" valuePropName="checked">
               <Row justify="space-between" align="middle">
                  <Checkbox disabled={status === 'loading'}>Remember me</Checkbox>
                  <Button type="primary" htmlType="submit" loading={status === 'loading'}>
                     Sign In
                  </Button>
               </Row>
            </Form.Item>
            <Row justify="space-between" align="middle">
               <Link to="/forgot">Forgot password</Link>
               <Link to="/register">Create new account</Link>
            </Row>
         </Form>
         <div style={{ textAlign: 'center', marginBottom: '12px' }}>or</div>
         <Link to="">
            <Button type="primary" style={{ width: '100%' }}>
               Continue with facebook
            </Button>
         </Link>
      </div>
   );
};

export default LoginPage;
