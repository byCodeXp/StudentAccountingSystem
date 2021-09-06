import React from 'react';
import { Link } from 'react-router-dom';
import { Button, Layout, Row, Space, Avatar, Typography, Menu, Dropdown, Badge } from 'antd';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { selectUser, logout } from '../features/identitySlice';
import { BellOutlined } from '@ant-design/icons';

export const Header = (props: { user: IUser | null }) => {
   const dispatch = useAppDispatch();

   const user = useAppSelector(selectUser);
   const name = `${user?.firstName} ${user?.lastName}`;

   const handleClickLogout = () => {
      dispatch(logout());
   };

   const menu = (
      <Menu style={{ marginTop: 16 }}>
         <Menu.Item key="1" disabled>
            <Typography.Text>
               Signed as <b>{user?.firstName + ' ' + user?.lastName}</b>
            </Typography.Text>
         </Menu.Item>
         <Menu.Item key="2">
            <Link to="/profile">Profile</Link>
         </Menu.Item>
         <Menu.Item key="3">
            <Link to="/settings">Settings</Link>
         </Menu.Item>
         {props.user?.role === 'Admin' && (
            <Menu.Item key="4">
               <Link to="/admin">Admin panel</Link>
            </Menu.Item>
         )}
         <Menu.Divider />
         <Menu.Item onClick={handleClickLogout} key="5">
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
               <Link to="/catalog/1">Catalog</Link>
               <Link to="/about">About</Link>
               <Link to="/contact">Contact</Link>
            </div>
            {!props.user && (
               <Link to="/login">
                  <Button type="link">Login</Button>
               </Link>
            )}
            {props.user && (
               <div
                  style={{
                     display: 'flex',
                     placeItems: 'center',
                  }}
               >
                  <div style={{ marginRight: 32 }}>
                     <Badge dot>
                        <BellOutlined style={{ color: '#fff' }} />
                     </Badge>
                  </div>
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
               </div>
            )}
         </Row>
      </Layout.Header>
   );
};
