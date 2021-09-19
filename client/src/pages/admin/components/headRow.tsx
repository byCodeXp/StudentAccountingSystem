import { Button, Row } from 'antd';
import React from 'react';

export const HeadRow = (props: { title: string, onClick?: any }) => {
   return (
      <Row justify="space-between">
         <h1>{props.title}</h1>
         {props.onClick && (<Button type="primary" onClick={() => props.onClick()}>Add</Button>)}
      </Row>
   );
};