import { Col, Row } from 'antd';
import React from 'react';

export const Container = (props: { children: any }) => {
   return (
      <Row justify="center">
         <Col xxl={12} xl={13} lg={14} md={15} xs={24}>
            {props.children}
         </Col>
      </Row>
   );
}