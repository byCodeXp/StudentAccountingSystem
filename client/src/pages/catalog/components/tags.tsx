import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { AppstoreOutlined } from '@ant-design/icons';
import { Checkbox, Collapse, List, Space, Typography } from 'antd';
import { fetchCategories, selectCategories, selectTags, setTags } from '../../../features/catalogSlice';

export const Tags = () => {
   
   const dispatch = useAppDispatch();

   const categories = useAppSelector(selectCategories);
   const tags = useAppSelector(selectTags);

   const handleOnCheck = (category: string, check: boolean) => {
      const index = tags.findIndex((m) => m === category);

      if (index !== -1) {
         if (check === false) {
            dispatch(setTags([...tags.slice(0, index), ...tags.slice(index + 1)]))
         }
      } else {
         if (check === true) {
            dispatch(setTags([...tags, category]));
         }
      }
   }

   useEffect(() => {
      dispatch(fetchCategories({ search: '' }));
   }, [dispatch]);

   return (
      <Collapse defaultActiveKey={['1']}>
         <Collapse.Panel
            header={
               <Space>
                  <AppstoreOutlined />
                  <Typography.Text>Technologies</Typography.Text>
               </Space>
            }
            key="1"
         >
            <List>
               {categories.map((category, index) => (
                  <List.Item key={index}>
                     <Checkbox
                        onChange={(e) =>
                           handleOnCheck(category.name, e.target.checked)
                        }
                     >
                        {category.name}
                     </Checkbox>
                  </List.Item>
               ))}
            </List>
         </Collapse.Panel>
      </Collapse>
   );
};
