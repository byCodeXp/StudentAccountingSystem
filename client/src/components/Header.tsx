import React from 'react';
import { Link } from 'react-router-dom';
import {
   Row,
   Layout,
   Button,
   Avatar,
   Dropdown,
   Menu,
   Space,
   Typography,
} from 'antd';

export const Header = (props: {
   signed: boolean;
   user?: IUser;
   onLogout: any;
}) => {
   const menu = (
      <Menu style={{ marginTop: 16 }}>
         <Menu.Item key="1" disabled>
            <Typography.Text>
               Signed as{' '}
               <b>{`${props.user?.firstName} ${props.user?.lastName}`}</b>
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
         <Menu.Item key="5" onClick={props.onLogout}>
            Log out
         </Menu.Item>
      </Menu>
   );

   return (
      <Layout.Header>
         <Row justify="space-between" align="middle" style={{ height: 64 }}>
            <Button
               type="link"
               style={{ fontSize: 18, display: 'flex', alignItems: 'center' }}
            >
               StudentProgress
            </Button>
            <div
               style={{
                  display: 'flex',
                  gap: 24,
                  textDecoration: 'underline',
                  color: '#ffffff',
               }}
            >
               <Link to="/" style={{ color: '#ffffff' }}>
                  Home
               </Link>
               <Link to="/catalog/1">Catalog</Link>
               <Link to="/about" style={{ color: '#ffffff' }}>
                  About
               </Link>
            </div>
            {props.signed === true ? (
               <Space align="center">
                  <Dropdown overlay={menu} trigger={['click']}>
                     <Button type="text" style={{ padding: 0 }}>
                        <Space
                           style={{ cursor: 'pointer' }}
                           className="ant-dropdown-link"
                        >
                           <Avatar
                              style={{
                                 color: '#f56a00',
                                 backgroundColor: '#fde3cf',
                              }}
                           >
                              {props.user?.firstName.slice(0, 1).toUpperCase()}
                           </Avatar>
                        </Space>
                     </Button>
                  </Dropdown>
               </Space>
            ) : (
               <Link to="/login">
                  <Button type="link">Login</Button>
               </Link>
            )}
         </Row>
      </Layout.Header>
   );
};
