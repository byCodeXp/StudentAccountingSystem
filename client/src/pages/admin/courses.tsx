import React, { useEffect, useState } from 'react';
import { Button, Row, Table, Modal, Input, Typography } from 'antd';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { addCourseAsync, getCoursesAsync, selectCourses } from '../../features/courseSlice';

const Courses = () => {
   const dispatch = useAppDispatch();

   const courses = useAppSelector(selectCourses);

   const request = {
      page: 1,
      perPage: 12,
      sortBy: 0,
      categories: [],
   };

   useEffect(() => {
      dispatch(getCoursesAsync(request));
   }, []);

   const [course, setCourse] = useState({
      id: '00000000-0000-0000-0000-000000000000',
      name: '',
      preview: '',
      description: '',
      categories: [],
   });

   const titleOnChange = (event: any) => {
      setCourse({ ...course, name: event.target.value });
   };

   const descriptionOnChange = (event: any) => {
      setCourse({ ...course, description: event.target.value });
   };

   const previewOnChange = (event: any) => {
      setCourse({ ...course, preview: event.target.value });
   };

   const [isModalVisible, setIsModalVisible] = useState(false);

   const showModal = () => setIsModalVisible(true);
   const handleCancel = () => setIsModalVisible(false);
   const handleOk = () => {
      dispatch(addCourseAsync(course));
      setIsModalVisible(false);
   };

   return (
      <>
         <Row justify="space-between">
            <h1>Courses</h1>
            <Button onClick={showModal}>Add</Button>
         </Row>
         <Modal
            title="Add new course"
            visible={isModalVisible}
            onCancel={handleCancel}
            onOk={handleOk}
         >
            <Typography.Text>Title</Typography.Text>
            <Input style={{ marginBottom: 24 }} onChange={titleOnChange} />
            <Typography.Text>Description</Typography.Text>
            <Input.TextArea style={{ marginBottom: 24 }} onChange={descriptionOnChange} />
            <Typography.Text>Preview</Typography.Text>
            <Input style={{ marginBottom: 24 }} onChange={previewOnChange} />
         </Modal>
         <Table
            dataSource={courses}
            columns={[
               { title: 'Title', dataIndex: 'name', key: 'name' },
               { title: 'Description', dataIndex: 'description', key: 'description' },
            ]}
         />
      </>
   );
};

export default Courses;
