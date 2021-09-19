import { useEffect, useState } from 'react';
import {
   Button,
   Collapse,
   Form,
   Input,
   Modal,
   Row,
   Table,
   TablePaginationConfig,
   Typography,
   Select,
   notification,
   Popover,
   Image,
   Tag,
   Space,
} from 'antd';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import {
   addCourseAsync,
   deleteCourseAsync,
   getCoursesAsync,
   resetStatusCourse,
   selectCount,
   selectCourses,
   selectStatus,
   updateCourseAsync,
} from '../../../features/courseSlice';
import { EditOutlined, EyeOutlined } from '@ant-design/icons';
import {
   getCategoriesAsync,
   selectCategories,
} from '../../../features/categorySlice';
import { HeadRow } from '../components/headRow';

export const CourseTab = () => {
   const dispatch = useAppDispatch();

   const categories = useAppSelector(selectCategories);

   const courses = useAppSelector(selectCourses);
   const countCourses = useAppSelector(selectCount);
   const statusCourse = useAppSelector(selectStatus);

   const [options, setOptions] = useState({ page: 1, perPage: 8 });

   const handlePagination = (pagination: TablePaginationConfig) => {
      setOptions({
         page: pagination.current ?? 1,
         perPage: pagination.pageSize ?? 4,
      });
   };

   const [editable, setEditable] = useState<string | undefined>(undefined);

   const [mode, setMode] = useState<'idle' | 'edit' | 'add'>('idle');

   const handleOnAdd = () => {
      form.resetFields();
      setMode('add');
   };

   const handleOnEdit = (id: string) => {
      const course = courses.find((m) => m.id === id);
      if (course) {
         const item = {
            ...course,
            categories: course.categories.map((m) => m.name),
         };
         setEditable(id);
         form.setFieldsValue(item);
         setMode('edit');
      }
   };

   const handleOnFinish = (values: any) => {
      const categorySet: Array<ICategory> = [];

      values.categories?.forEach((item: string) => {
         const category = categories.find((m) => m.name === item);
         if (category) {
            categorySet.push(category);
         }
      });

      const course: ICourse = {
         ...values,
         categories: categorySet,
      };

      console.log(course);

      if (mode === 'edit') {
         dispatch(updateCourseAsync(course));
      }
      if (mode === 'add') {
         dispatch(addCourseAsync(course));
      }
      setMode('idle');
   };

   const handleOnDelete = () => {
      setMode('idle');
      if (editable) {
         dispatch(deleteCourseAsync(editable));
      }
   };

   const handleOnCancel = () => {
      setMode('idle');
   };

   const [form] = Form.useForm();

   useEffect(() => {
      dispatch(getCategoriesAsync());
   }, [dispatch]);

   useEffect(() => {
      dispatch(
         getCoursesAsync({
            page: options.page ?? 1,
            perPage: options.perPage ?? 1,
            sortBy: 'New',
            categories: [],
         })
      );
   }, [dispatch, options]);

   useEffect(() => {
      if (statusCourse === 'deleted') {
         notification.success({
            placement: 'bottomRight',
            message: 'Deleted',
            description: `Course ${123} was deleted`,
         });
         dispatch(resetStatusCourse());
      }
   }, [statusCourse]);

   return (
      <>
         <HeadRow title="Courses" onClick={handleOnAdd} />
         <Table
            pagination={{
               total: countCourses,
               showSizeChanger: true,
               pageSizeOptions: ['4', '8', '16', '32'],
               defaultPageSize: options.perPage,
            }}
            onChange={handlePagination}
            dataSource={courses}
            columns={[
               {
                  title: 'Preview',
                  dataIndex: 'preview',
                  key: 'preview',
                  render: (preview) => (
                     <Popover content={<Image width={256} src={preview} />}>
                        <Button type="text"><EyeOutlined /></Button>
                     </Popover>
                  ),
               },
               {
                  title: 'Name',
                  dataIndex: 'name',
                  key: 'name',
               },
               {
                  title: 'Categories',
                  dataIndex: 'categories',
                  key: 'categories',
                  render: (categories: ICategory[]) => (
                     <Space>
                        {categories.map((category, index) => <Tag key={index} color={category.color}>{category.name}</Tag>)}
                     </Space>
                  )
               },
               {
                  title: 'Description',
                  dataIndex: 'description',
                  key: 'description',
               },
               {
                  title: '',
                  dataIndex: '',
                  key: '',
                  render: (item: ICourse) => (
                     <Button
                        type="dashed"
                        onClick={() => handleOnEdit(item.id)}
                     >
                        <EditOutlined />
                     </Button>
                  ),
               },
            ]}
         />
         <Modal
            onCancel={handleOnCancel}
            title={`${mode === 'add' ? 'Add' : 'Edit'} course`}
            visible={mode !== 'idle'}
            footer={[
               <Form.Item>
                  <Button onClick={handleOnCancel}>Cancel</Button>
                  <Button
                     type="primary"
                     form="editForm"
                     key="submit"
                     htmlType="submit"
                  >
                     OK
                  </Button>
               </Form.Item>,
            ]}
         >
            <Form
               id="editForm"
               name="basic"
               autoComplete="off"
               form={form}
               onFinish={handleOnFinish}
            >
               <Form.Item name="id" hidden>
                  <Input />
               </Form.Item>
               <Typography>Name</Typography>
               <Form.Item
                  name="name"
                  rules={[{ required: true, message: 'Please input name!' }]}
               >
                  <Input />
               </Form.Item>
               <Typography>Description:</Typography>
               <Form.Item
                  name="description"
                  rules={[
                     { required: true, message: 'Please input description!' },
                  ]}
               >
                  <Input.TextArea />
               </Form.Item>
               <Typography>Preview:</Typography>
               <Form.Item
                  name="preview"
                  rules={[{ required: true, message: 'Please input preview!' }]}
               >
                  <Input />
               </Form.Item>
               <Form.Item name="categories">
                  <Select
                     mode="multiple"
                     style={{ width: '100%' }}
                     placeholder="Select category"
                     optionLabelProp="label"
                  >
                     {categories.map((category, index) => (
                        <Select.Option
                           key={index}
                           value={category.name}
                           label={category.name}
                        >
                           <div className="demo-option-label-item">
                              {category.name}
                           </div>
                        </Select.Option>
                     ))}
                  </Select>
               </Form.Item>
            </Form>
            {mode === 'edit' && (
               <Collapse>
                  <Collapse.Panel key={1} header="Remove">
                     <Row justify="space-between" align="middle">
                        <Typography>Are you sure ?</Typography>
                        <Button onClick={handleOnDelete} danger>
                           Remove
                        </Button>
                     </Row>
                  </Collapse.Panel>
               </Collapse>
            )}
         </Modal>
      </>
   );
};
