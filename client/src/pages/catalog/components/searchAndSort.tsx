import React from 'react';
import { Input, Select, Space } from 'antd';
import { useAppDispatch } from '../../../app/hooks';
import { changeSort, setSearch } from '../../../features/catalogSlice';

const { Option } = Select;

export const SearchAndSort = () => {
   const dispatch = useAppDispatch();

   let timeout: NodeJS.Timeout | undefined = undefined;

   const handleSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
      if (timeout) {
         window.clearTimeout(timeout);
      }

      timeout = setTimeout(() => {
         dispatch(setSearch(event.target.value));
      }, 500);
   }

   return (
      <Input
         onChange={handleSearch}
         placeholder="Type to find here.."
         addonAfter={
            <Space size={20}>
               <span>Sort by:</span>
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
            </Space>
         }
      />
   );
};
