import React from 'react';
import { Select } from 'antd';
import { useAppDispatch } from '../../../app/hooks';
import { changeSort } from '../../../features/catalogSlice';

const { Option } = Select;

export const Sort = () => {
   const dispatch = useAppDispatch();

   return (
      <Select
         defaultValue={'Relevance'}
         onChange={(value) => dispatch(changeSort(value))}
         className="select-after"
         style={{ textAlign: 'left', width: 140 }}
      >
         <Option value="Relevance">Relevance</Option>
         <Option value="New">New</Option>
         <Option value="Popular">Popular</Option>
         <Option value="Alphabetically">Alphabetically</Option>
      </Select>
   );
};
