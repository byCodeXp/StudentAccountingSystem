import React, { useState } from 'react';
import { Menu, Row, Col } from 'antd';
import { AppstoreOutlined, HddOutlined, UserOutlined, ProjectOutlined } from '@ant-design/icons';
import Users from './users';
import Courses from './courses';

const { SubMenu } = Menu;

const TabContent = (element: { title: string; content: any }) => {
   const { title, content } = element;
   return <>{content}</>;
};

const items = [
   { title: 'Dashboard', content: null },
   { title: 'Users', content: <Users /> },
   { title: 'Courses', content: <Courses /> },
];

const Admin = () => {
   const [active, setActive] = useState(0);

   const openTab = (index: number) => setActive(index);

   return (
      <Row gutter={32}>
         <Col span={4}>
            <Menu mode="inline">
               <Menu.Item onClick={() => openTab(0)} icon={<AppstoreOutlined />}>
                  Dashboard
               </Menu.Item>
               <SubMenu key="users" title="Users" icon={<UserOutlined />}>
                  <Menu.Item onClick={() => openTab(1)}>All</Menu.Item>
                  <Menu.Item>Statistics</Menu.Item>
               </SubMenu>
               <SubMenu key="courses" title="Courses" icon={<HddOutlined />}>
                  <Menu.Item onClick={() => openTab(2)}>All</Menu.Item>
                  <Menu.Item>Statistics</Menu.Item>
               </SubMenu>
               <SubMenu key="stats" title="Statistics" icon={<ProjectOutlined />}>
                  <Menu.Item>All</Menu.Item>
                  <Menu.Item>Reports</Menu.Item>
               </SubMenu>
            </Menu>
         </Col>
         <Col span={20}>
            {items[active] && (
               <TabContent title={items[active].title} content={items[active].content} />
            )}
         </Col>
      </Row>
   );
};

export default Admin;
