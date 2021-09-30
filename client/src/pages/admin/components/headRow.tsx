import React from 'react';
import { Button, Row, Input } from 'antd';

export const HeadRow = (props: { title: string, onClick?: any, onSearch?: any }) => {
   const handleOnSearch = (event: any) => {
      props.onSearch(event.target.value);
   }

   return (
      <Row justify="space-between" style={{ marginBottom: 16 }}>
         <div style={{ flex: 1 }}>
            <Input onChange={handleOnSearch} placeholder="Search" />
         </div>
         {props.onClick && (<Button style={{ marginLeft: 16 }} type="primary" onClick={() => props.onClick()}>Add</Button>)}
      </Row>
   );
};