import React, { useEffect } from 'react';
import { Menu, Row, Col, Table } from 'antd';
import { AppstoreOutlined, HddOutlined, UserOutlined, ProjectOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { getCoursesAsync, getUsersAsync, selectCourses, selectUsers } from '../../features/admin/adminSlice';
import { selectUser } from '../../features/user/userSlice';
import { Navigate } from 'react-router-dom';

const { SubMenu } = Menu;

const Admin = () => {
   const dispatch = useAppDispatch();

   const courses = useAppSelector(selectCourses);
   const users = useAppSelector(selectUsers);
   const user = useAppSelector(selectUser);

   useEffect(() => {
      dispatch(getCoursesAsync(1));
      dispatch(getUsersAsync(1));
   }, []);

   if (user.role != 'Admin') {
      return <Navigate to="/" />;
   }

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
            <Table
               dataSource={users}
               columns={[
                  { title: 'First name', dataIndex: 'firstName', key: 'firstName' },
                  { title: 'Last Name', dataIndex: 'lastName', key: 'lastName' },
                  { title: 'Age', dataIndex: 'age', key: 'age' },
                  { title: 'Email', dataIndex: 'email', key: 'email' },
               ]}
            />
            <h1>Courses</h1>
            <Table
               dataSource={courses}
               columns={[
                  { title: 'Title', dataIndex: 'name', key: 'name' },
                  { title: 'Description', dataIndex: 'description', key: 'description' },
               ]}
            />
            <h1>Statistics</h1>
         </Col>
      </Row>
   );
};

export default Admin;
