import { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { PageHeader, Row, Col, Tag, Button, DatePicker } from 'antd';
import { getOneCourseAsync, selectCurrentCourse, } from '../../features/courseSlice';
import { useParams } from 'react-router-dom';
import { BellOutlined } from '@ant-design/icons';
import { subscribeOnCourseAsync, unsubscribeCourseAsync } from '../../features/identitySlice';
import { getUserCoursesAsync, selectUserCourses } from '../../features/identitySlice';
import moment, { now } from 'moment';

export const DetailsPage = () => {
   const dispatch = useAppDispatch();

   const course = useAppSelector(selectCurrentCourse);

   const { id } = useParams();

   const myCourses = useAppSelector(selectUserCourses);

   useEffect(() => {
      dispatch(getUserCoursesAsync());
      dispatch(getOneCourseAsync(id));
   }, []);

   const [date, setDate] = useState('');

   const handleOnSubscribe = () => {
      if (course) {
         dispatch(subscribeOnCourseAsync({ course: course, date: date }));
      }
   };

   const handleOnUnsubscribe = () => {
      if (course) {
         dispatch(unsubscribeCourseAsync(course));
      }
   }

   const handleOnDate = (date: any, dateString: any) => {
      setDate(dateString);
   };

   const handleSubscribeAction = () => {
      subStatus() ? handleOnUnsubscribe() : handleOnSubscribe();
   }

   const subStatus = (): boolean => myCourses.some(m => m.id === id);

   return (
      <Row>
         <Col xxl={{ span: 14, offset: 5 }} xl={{ span: 16, offset: 4 }} lg={{ span: 20, offset: 2 }}>
            <PageHeader
               className="site-page-header-responsive"
               title={course?.name}
               onBack={() => window.history.back()}
               tags={course?.categories.map((category, index) => (
                  <Tag key={index} color={category.color}>
                     {category.name}
                  </Tag>
               ))}
               extra={[
                  !subStatus() && <DatePicker key="1" onChange={handleOnDate} disabledDate={(currentDate: moment.Moment) => currentDate < moment(now())}/>,
                  <Button onClick={handleSubscribeAction} key="2">
                     <BellOutlined /> {subStatus() ? 'Unsubscribe' : 'Subscribe'}
                  </Button>
               ]}
            >
               <Row gutter={32}>
                  <Col span={24}>
                     <img
                        style={{ width: '100%' }}
                        src={course?.preview}
                        alt={course?.name}
                     />
                     <p style={{ marginTop: 16 }}>{course?.description}</p>
                  </Col>
               </Row>
            </PageHeader>
         </Col>
      </Row>
   );
};
