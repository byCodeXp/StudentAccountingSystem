import React, { useState } from 'react';
import { Col, Menu, Row } from 'antd';
import {
   AppstoreOutlined,
   HddOutlined,
   UserOutlined,
   NumberOutlined,
} from '@ant-design/icons';
import { CourseTab } from './sections/course';
import { CategorySection } from './sections/category';
import { UserSection } from './sections/user';

const items = [
   { title: 'Dashboard', icon: <AppstoreOutlined />, content: null },
   { title: 'Users', icon: <UserOutlined />, content: <UserSection /> },
   { title: 'Courses', icon: <HddOutlined />, content: <CourseTab /> },
   {
      title: 'Categories',
      icon: <NumberOutlined />,
      content: <CategorySection />,
   },
];

export const AdminPage = () => {
   const [active, setActive] = useState(0);

   const openTab = (index: number) => setActive(index);

   return (
      <Row gutter={32}>
         <Col span={4}>
            <Menu mode="inline">
               {items.map((item, index) => (
                  <Menu.Item
                     key={index}
                     icon={item.icon}
                     onClick={() => openTab(index)}
                  >
                     {item.title}
                  </Menu.Item>
               ))}
            </Menu>
         </Col>
         <Col span={20}>{items[active].content}</Col>
      </Row>
   );
};
