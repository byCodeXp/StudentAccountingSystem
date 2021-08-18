import React from 'react';
import { Tabs, Row, Col } from 'antd';

const { TabPane } = Tabs;

const Settings = () => {
   return (
      <Row justify="center">
         <Col span={10}>
            <Tabs tabPosition="left" type="card">
               <TabPane tab="Personal information" key="1">
                  Content of Tab 1
               </TabPane>
               <TabPane tab="Security" key="2">
                  Content of Tab 2
               </TabPane>
               <TabPane tab="Payment" key="3">
                  Content of Tab 3
               </TabPane>
               <TabPane tab="Account" key="4">
                  Content of Tab 4
               </TabPane>
            </Tabs>
         </Col>
      </Row>
   );
};

export default Settings;
