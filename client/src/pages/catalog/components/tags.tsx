import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { AppstoreOutlined } from '@ant-design/icons';
import { Checkbox, Collapse, Form, List, Space, Typography } from 'antd';
import { fetchCategories, selectCategories, setTags } from '../../../features/catalogSlice';
import { useForm } from 'antd/lib/form/Form';

export const Tags = () => {
   const dispatch = useAppDispatch();

   const categories = useAppSelector(selectCategories);

   const handleFinish = () => {
      const tags = form.getFieldsValue();
      const array = Object.keys(tags).filter((item) => tags[item] === true);
      dispatch(setTags(array));
   };

   const [form] = useForm();

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
               <Form form={form} onChange={handleFinish}>
                  {categories.map((category, index) => (
                     <List.Item key={index}>
                        <Form.Item noStyle name={category.name} valuePropName="checked">
                           <Checkbox>{category.name}</Checkbox>
                        </Form.Item>
                     </List.Item>
                  ))}
               </Form>
            </List>
         </Collapse.Panel>
      </Collapse>
   );
};
