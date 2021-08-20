import React, { useEffect } from 'react';
import 'jquery/dist/jquery.min';
import 'packery/dist/packery.pkgd.min';
import { Row, Col, Input, Select, Pagination, Space, Card } from 'antd';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { getCoursesAsync, selectCourses, selectStatus } from '../../features/course/courseSlice';
import { Link } from 'react-router-dom';
import { FilterComponent } from './filter';

const { Option } = Select;
const { Meta } = Card;

const selectAfter = (
   <Space size={20}>
      <span>Sort by:</span>
      <Select defaultValue="relevance" className="select-after" style={{ textAlign: 'left', width: 104 }}>
         <Option value="relevance">Relevance</Option>
         <Option value="popular">Popular</Option>
         <Option value="new">New</Option>
         <Option value="trending">Trending</Option>
      </Select>
   </Space>
);

function Catalog() {
   const dispatch = useAppDispatch();

   const status = useAppSelector(selectStatus);
   const courses = useAppSelector(selectCourses);

   useEffect(() => {
      dispatch(getCoursesAsync(1));
   }, [dispatch]);

   const courseSet = () => {
      if (status !== 'loading') {
         return courses.map((course: ICourse, index) => (
            <Col key={index} span={6}>
               <Link to={`/course/${course.id}`}>
                  <Card
                     style={{ width: '100%', marginBottom: 24 }}
                     hoverable
                     cover={<img alt={course.title} src={course.preview} />}
                  >
                     <Meta title={course.title} description={course.description} />
                  </Card>
               </Link>
            </Col>
         ));
      }
   };

   return (
      <Row gutter={32}>
         <Col span={20}>
            <Input addonAfter={selectAfter} />
            <Row style={{ marginTop: 24 }} gutter={24}>
               {courseSet()}
            </Row>
            <Pagination showSizeChanger={false} defaultPageSize={16} total={256} />
         </Col>
         <Col span={4}>
            <FilterComponent />
         </Col>
      </Row>
   );
}

export default Catalog;
