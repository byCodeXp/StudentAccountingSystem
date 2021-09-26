import React from 'react';
import { UserOutlined, SettingOutlined } from '@ant-design/icons';
import { Row, Col, Space, Avatar, Typography, Button, Divider, Tabs } from 'antd';
import { Link } from 'react-router-dom';
import { useAppSelector } from '../../app/hooks';
import { selectUser } from '../../features/identitySlice';

const { TabPane } = Tabs;

export const ProfilePage = () => {
   const user = useAppSelector(selectUser);

   return (
      <Row justify="center">
         <Col span={10}>
            <Row justify="space-between" align="middle">
               <Space>
                  <Avatar shape="square" size={48} icon={<UserOutlined />} />
                  <Typography.Text>{`${user?.firstName} ${user?.lastName}`}</Typography.Text>
               </Space>
               <Link to="/settings">
                  <Button shape="circle" icon={<SettingOutlined />} />
               </Link>
            </Row>
            <Divider />
         </Col>
      </Row>
   )
};