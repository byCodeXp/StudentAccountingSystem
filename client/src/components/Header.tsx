import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Layout, Row, Space, Avatar, Typography, Menu, Dropdown } from 'antd';
import { logout } from '../features/user/userSlice';
import { useAppDispatch } from '../app/hooks';

export const Header = (props: { user: IUser }) => {
   const dispatch = useAppDispatch();

   const [name, setName] = useState('');

   useEffect(() => {
      const { firstName, lastName, email } = props.user;
      if (firstName === '' && lastName === '') {
         setName(email);
      } else {
         setName(`${firstName} ${lastName}`);
      }
   }, [props.user]);

   const menu = (
      <Menu style={{ marginTop: 16 }}>
         <Menu.Item key="1" disabled>
            <Typography.Text>
               Signed as <b>{name}</b>
            </Typography.Text>
         </Menu.Item>
         <Menu.Item key="2">
            <Link to="/profile">Profile</Link>
         </Menu.Item>
         <Menu.Item key="3">
            <Link to="/settings">Settings</Link>
         </Menu.Item>
         <Menu.Divider />
         <Menu.Item onClick={() => dispatch(logout())} key="4">
            Log out
         </Menu.Item>
      </Menu>
   );

   return (
      <Layout.Header>
         <Row justify="space-between" align="middle">
            <Link to="/">
               <Button type="link">StudentProgress</Button>
            </Link>
            <div style={{ display: 'flex', gap: 24 }}>
               <Link to="/">Home</Link>
               <Link to="/catalog">Catalog</Link>
               <Link to="/about">About</Link>
               <Link to="/contact">Contact</Link>
            </div>
            {props.user.email === '' ? (
               <Link to="/login">
                  <Button type="link">Login</Button>
               </Link>
            ) : (
               <Space align="center">
                  <Dropdown overlay={menu} trigger={['click']}>
                     <a className="ant-dropdown-link" onClick={(e) => e.preventDefault()}>
                        <Space>
                           <Avatar style={{ color: '#f56a00', backgroundColor: '#fde3cf' }}>
                              {name.slice(0, 1).toUpperCase()}
                           </Avatar>
                        </Space>
                     </a>
                  </Dropdown>
               </Space>
            )}
         </Row>
      </Layout.Header>
   );
};
