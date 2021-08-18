import React from 'react';
import 'jquery/dist/jquery.min';
import 'packery/dist/packery.pkgd.min';
import {
   Row,
   Col,
   Input,
   Checkbox,
   Select,
   Pagination,
   Collapse,
   List,
   Typography,
   Space,
} from 'antd';
import { PoundOutlined, AppstoreOutlined } from '@ant-design/icons';
import { courses } from './MockData';
import CatalogCard from '../components/CatalogCard';

const { Option } = Select;
const { Panel } = Collapse;

function Catalog() {
   const selectAfter = (
      <Space size={20}>
         <span>Sort by:</span>
         <Select
            defaultValue="relevance"
            className="select-after"
            style={{ textAlign: 'left', width: 104 }}
         >
            <Option value="relevance">Relevance</Option>
            <Option value="popular">Popular</Option>
            <Option value="new">New</Option>
            <Option value="trending">Trending</Option>
         </Select>
      </Space>
   );

   return (
      <Row gutter={32}>
         <Col span={20}>
            <Input addonAfter={selectAfter} />
            <Row
               style={{ marginTop: 24 }}
               gutter={24}
               data-packery='{ "originLeft: false" }'
            >
               {courses.map((course, index) => (
                  <Col span={6}>
                     <CatalogCard key={index} course={course} />
                  </Col>
               ))}
            </Row>
            <Pagination
               showSizeChanger={false}
               defaultPageSize={16}
               total={256}
            />
         </Col>
         <Col span={4}>
            <Collapse>
               <Panel
                  header={
                     <Space>
                        <PoundOutlined />
                        <Typography.Text>Price</Typography.Text>
                     </Space>
                  }
                  key="1"
               >
                  <List>
                     <List.Item>
                        <Checkbox>Free</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>Paid</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>Sale</Checkbox>
                     </List.Item>
                  </List>
               </Panel>
               <Panel
                  header={
                     <Space>
                        <AppstoreOutlined />
                        <Typography.Text>Technologies</Typography.Text>
                     </Space>
                  }
                  key="2"
               >
                  <List>
                     <List.Item>
                        <Checkbox>.NET</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>React</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>Svelte</Checkbox>
                     </List.Item>
                  </List>
               </Panel>
            </Collapse>
         </Col>
      </Row>
   );
}

export default Catalog;
