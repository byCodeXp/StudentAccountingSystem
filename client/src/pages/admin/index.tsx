import { useState, useEffect } from 'react';
import { Col, Menu, Row } from 'antd';
import { HddOutlined, UserOutlined, NumberOutlined, } from '@ant-design/icons';
import { CourseTab } from './sections/course';
import { CategorySection } from './sections/category';
import { UserSection } from './sections/user';

const items = [
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

   const [width, setWidth] = useState(window.innerWidth);

   useEffect(() => window.onresize = () => setWidth(window.innerWidth), []);

   return (
      <Row gutter={32}>
         <Col xxl={{ span: 4 }} xl={{ span: 5 }} span={24} style={{ marginBottom: 32 }}>
            <Menu mode={width <= 1200 ? 'horizontal' : 'inline'}>
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
         <Col xxl={{ span: 20 }} xl={{ span: 19 }} span={24}>{items[active].content}</Col>
      </Row>
   );
};
