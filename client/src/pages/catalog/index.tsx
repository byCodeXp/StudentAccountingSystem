import React, { useEffect, useRef, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import {
   getCoursesAsync,
   selectCount,
   selectCourses,
   selectStatus as selectCourseStatus,
} from '../../features/courseSlice';
import {
   Row,
   Col,
   Input,
   Select,
   Pagination,
   Space,
   Card,
   Collapse,
   Typography,
   List,
   Checkbox,
} from 'antd';
import { Link, Navigate, useParams, useNavigate } from 'react-router-dom';
import { AppstoreOutlined, PoundOutlined } from '@ant-design/icons';
import {
   getCategoriesAsync,
   selectCategories,
   selectStatus as selectCategoryStatus,
} from '../../features/categorySlice';

const { Panel } = Collapse;
const { Option } = Select;
const { Meta } = Card;

const Catalog = () => {
   const dispatch = useAppDispatch();

   const courseStatus = useAppSelector(selectCourseStatus);
   const categoryStatus = useAppSelector(selectCategoryStatus);
   const courses = useAppSelector(selectCourses);
   const totalCount = useAppSelector(selectCount);
   const categories = useAppSelector(selectCategories);

   const { page } = useParams();
   const [sort, setSort] = useState('0');

   const navigate = useNavigate();

   useEffect(() => {
      dispatch(
         getCoursesAsync({
            page: parseInt(page ?? '1'),
            perPage: 2,
            sortBy: parseInt(sort ?? '0'),
            categories: [],
         })
      );
   }, [sort, page]);

   useEffect(() => {
      dispatch(getCategoriesAsync());
   }, []);

   const courseSet = () => {
      if (courseStatus === 'success') {
         return courses.map((course: ICourse, index) => (
            <Col key={index} span={6}>
               <Link to={`/course/${course.id}`}>
                  <Card
                     style={{ width: '100%', marginBottom: 24 }}
                     hoverable
                     cover={<img alt={course.name} src={course.preview ?? 'https://cdn.dribbble.com/users/1753953/screenshots/3818675/animasi-emptystate.gif'} />}
                  >
                     <Meta title={course.name} description={course.description} />
                  </Card>
               </Link>
            </Col>
         ));
      }
   };

   const categoriesSet = () => {
      if (categoryStatus === 'success') {
         return categories.map((category: ICategory, index) => (
            <List.Item key={index}>
               <Checkbox>{category.name}</Checkbox>
            </List.Item>
         ));
      }
   };

   const handleChange = (value: string) => {
      setSort(value);
   };

   const navigateToPage = (page: number) => {
      navigate(`/catalog/${page}`);
   };

   return (
      <Row gutter={32}>
         <Col xs={{ span: 24 }} lg={{ span: 18 }} xl={{ span: 19 }} xxl={{ span: 20 }}>
            <Input
               placeholder="Type to find here.."
               addonAfter={
                  <Space size={20}>
                     <span>Sort by:</span>
                     <Select
                        value={sort}
                        onChange={handleChange}
                        className="select-after"
                        style={{ textAlign: 'left', width: 104 }}
                     >
                        <Option value="0">Relevance</Option>
                        <Option value="1">New</Option>
                        <Option value="2">Popular</Option>
                        <Option value="3">Alphabetically</Option>
                     </Select>
                  </Space>
               }
            />
            <Row style={{ marginTop: 24 }} gutter={24}>
               {courseSet()}
            </Row>
            <Pagination
               current={parseInt(page ?? '1')}
               onChange={navigateToPage}
               defaultPageSize={2}
               total={totalCount}
            />
         </Col>
         <Col xs={{ span: 0 }} lg={{ span: 6 }} xl={{ span: 5 }} xxl={{ span: 4 }}>
            <Collapse defaultActiveKey={['1', '2']}>
               <Panel
                  header={
                     <Space>
                        <PoundOutlined />
                        <Typography.Text>Price</Typography.Text>
                     </Space>
                  }
                  key="1"
               >
                  <List>
                     <List.Item>
                        <Checkbox>Free</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>Paid</Checkbox>
                     </List.Item>
                     <List.Item>
                        <Checkbox>Sale</Checkbox>
                     </List.Item>
                  </List>
               </Panel>
               <Panel
                  header={
                     <Space>
                        <AppstoreOutlined />
                        <Typography.Text>Technologies</Typography.Text>
                     </Space>
                  }
                  key="2"
               >
                  <List>{categoriesSet()}</List>
               </Panel>
            </Collapse>
         </Col>
      </Row>
   );
};

export default Catalog;
