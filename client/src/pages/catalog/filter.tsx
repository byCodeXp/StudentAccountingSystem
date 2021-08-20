import React from 'react';
import { Checkbox, Collapse, List, Space, Typography } from 'antd';
import { AppstoreOutlined, PoundOutlined } from '@ant-design/icons';

const { Panel } = Collapse;

export const FilterComponent = () => {
   return (
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
   );
};
