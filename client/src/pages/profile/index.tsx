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
            <Tabs defaultActiveKey="1">
               <TabPane tab="Recent activity" key="1">
                  Content of Tab Pane 1
               </TabPane>
               <TabPane tab="Subscribed courses" key="2">
                  Content of Tab Pane 2
               </TabPane>
               <TabPane tab="Comments" key="3">
                  Content of Tab Pane 3
               </TabPane>
               <TabPane tab="Something else" key="4">
                  Content of Tab Pane 4
               </TabPane>
            </Tabs>
         </Col>
      </Row>
   )
};