import React from 'react';
import { Row, Col, Typography, Space, Tabs, Divider, Button } from 'antd';
import { Avatar } from 'antd';
import { UserOutlined, SettingOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';

const { TabPane } = Tabs;

const Profile = () => {
   return (
      <Row justify="center">
         <Col span={10}>
            <Row justify="space-between" align="middle">
               <Space>
                  <Avatar shape="square" size={48} icon={<UserOutlined />} />
                  <Typography.Text>Username</Typography.Text>
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
   );
};

export default Profile;
