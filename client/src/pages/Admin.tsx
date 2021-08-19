import React from 'react';
import { Menu, Row, Col } from 'antd';
import { AppstoreOutlined, HddOutlined, UserOutlined, ProjectOutlined, DownOutlined } from '@ant-design/icons';
import { Table, Badge, Dropdown, Space } from 'antd';

const { SubMenu } = Menu;

const Admin = () => {
   const columns = [
      { title: 'First name', dataIndex: 'firstName', key: 'firstName' },
      { title: 'Second name', dataIndex: 'secondName', key: 'secondName' },
      { title: 'Email', dataIndex: 'email', key: 'email' },
      { title: 'Age', dataIndex: 'age', key: 'age' },
      { title: 'Count subscribed courses', dataIndex: 'countCourses', key: 'countCourses' },
   ];

   return (
      <Row gutter={32}>
         <Col span={4}>
            <Menu mode="inline">
               <Menu.Item icon={<AppstoreOutlined />}>Dashboard</Menu.Item>
               <SubMenu key="users" title="Users" icon={<UserOutlined />}>
                  <Menu.Item>All</Menu.Item>
                  <Menu.Item>Statistics</Menu.Item>
               </SubMenu>
               <SubMenu key="courses" title="Courses" icon={<HddOutlined />}>
                  <Menu.Item>All</Menu.Item>
                  <Menu.Item>Statistics</Menu.Item>
               </SubMenu>
               <SubMenu key="stats" title="Statistics" icon={<ProjectOutlined />}>
                  <Menu.Item>All</Menu.Item>
                  <Menu.Item>Reports</Menu.Item>
               </SubMenu>
            </Menu>
         </Col>
         <Col span={20}>
            <h1>Dashboard</h1>
            <h1>Users</h1>
            <Table columns={columns}></Table>
            <h1>Courses</h1>
            <Table></Table>
            <h1>Statistics</h1>
         </Col>
      </Row>
   );
};

export default Admin;
