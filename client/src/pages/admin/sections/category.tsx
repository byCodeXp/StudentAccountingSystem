import { useState, useEffect } from 'react';
import { EditOutlined } from '@ant-design/icons';
import { Button, Collapse, Form, Input, Modal, Row, Table, Typography, Tag } from 'antd';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { HeadRow } from '../components/headRow';
import { CompactPicker } from 'react-color';
import { selectCategories, loadCategoriesAsync, createCategoryAsync, updateCategoryAsync, deleteCategoryAsync } from '../../../features/adminSlice';

export const CategorySection = () => {
   const dispatch = useAppDispatch();

   const categories = useAppSelector(selectCategories);
   
   const [current, setCurrent] = useState('');
   const [mode, setMode] = useState<'idle' | 'edit' | 'add'>('idle');
   const [search, setSearch] = useState('');

   const [form] = Form.useForm();

   const handleOnAdd = () => {
      form.resetFields();
      setMode('add');
   };

   const handleOnEdit = (id: string) => {
      const item = categories.find(m => m.id === id);
      if (item) {
         form.setFieldsValue(item);
         setCurrent(item.id);
         setMode('edit');
      }
   };

   const handleOnFinish = (values: ICategory) => {
      if (mode === 'add') {
         dispatch(createCategoryAsync(values));
      }
      if (mode === 'edit') {
         dispatch(updateCategoryAsync(values));
      }
      setMode('idle');
   };

   const handleOnDelete = () => {
      setMode('idle');
      if (mode === 'edit') {
         dispatch(deleteCategoryAsync(current));
      }
   };

   const handleOnColorChange = (color: any) => {
      form.setFieldsValue({ ...form.getFieldsValue(), color: color.hex })
   }

   const handleOnSearch = (value: string) => {
      setSearch(value);
   }

   useEffect(() => {
      dispatch(loadCategoriesAsync({ search }));
   }, [dispatch, search])

   return (
      <>
         <HeadRow title="Categories" onClick={handleOnAdd} onSearch={handleOnSearch} />
         <Table
            pagination={{
               total: categories.length,
               showSizeChanger: true,
               pageSizeOptions: ['4', '8', '16', '32'],
               defaultPageSize: 4,
            }}
            dataSource={categories}
            rowKey={item => item.id}
            columns={[
               {
                  title: 'Name',
                  dataIndex: '',
                  key: 'name',
                  render: (item) => {
                     return <Tag color={item.color}>{item.name}</Tag>
                  }
               },
               {
                  title: '',
                  dataIndex: '',
                  key: '',
                  width: 1,
                  render: (item: ICategory) => (
                     <Button onClick={() => handleOnEdit(item.id)} type="dashed">
                        <EditOutlined />
                     </Button>
                  ),
               },
            ]}
         />
         <Modal
            onCancel={() => setMode('idle')}
            title={`${mode === 'add' ? 'Add' : 'Edit'} category`}
            visible={mode !== 'idle'}
            footer={[
               <Form.Item>
                  <Button onClick={() => setMode('idle')}>Cancel</Button>
                  <Button type="primary" form="editForm" htmlType="submit">
                     OK
                  </Button>
               </Form.Item>,
            ]}
         >
            <Form
               id="editForm"
               name="basic"
               autoComplete="off"
               onFinish={handleOnFinish}
               form={form}
            >
               <Form.Item name="id" hidden>
                  <Input />
               </Form.Item>
               <Typography>Name:</Typography>
               <Form.Item name="name">
                  <Input />
               </Form.Item>
               <Typography>Color:</Typography>
               <Form.Item name="color" hidden>
                  <Input />
               </Form.Item>
               <div style={{ height: 8 }}></div>
               <CompactPicker onChangeComplete={handleOnColorChange} />
            </Form>
            {mode === 'edit' && (
               <Collapse style={{ marginTop: 24 }}>
                  <Collapse.Panel key={1} header="Remove">
                     <Row justify="space-between" align="middle">
                        <Typography>Are you sure ?</Typography>
                        <Button onClick={handleOnDelete} danger>Remove</Button>
                     </Row>
                  </Collapse.Panel>
               </Collapse>
            )}
         </Modal>
      </>
   );
};
