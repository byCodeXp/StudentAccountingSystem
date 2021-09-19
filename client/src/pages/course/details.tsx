import { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { PageHeader, Row, Col, Tag, Button, DatePicker } from 'antd';
import {
   getOneCourseAsync,
   selectCurrentCourse,
   selectStatus,
} from '../../features/courseSlice';
import { useParams, Navigate } from 'react-router-dom';
import { BellOutlined } from '@ant-design/icons';
import { subscribeOnCourseAsync, unsubscribeCourseAsync } from '../../features/identitySlice';
import { getUserCoursesAsync, selectUser, selectUserCourses } from '../../features/identitySlice';

const categoriesColors = [
   { name: 'Angular', color: 'red' },
   { name: 'Node.js', color: 'green' },
   { name: 'Figma', color: 'purple' },
   { name: 'UI/UX', color: 'pink' },
   { name: 'Blender', color: 'orange' },
   { name: 'TypeScript', color: 'blue' },
   { name: 'DevOps', color: 'darkgray' },
   { name: 'DevOps', color: 'darkgray' },
   { name: 'Unity', color: 'yellow' },
];

export const DetailsPage = () => {
   const dispatch = useAppDispatch();

   const course = useAppSelector(selectCurrentCourse);
   const user = useAppSelector(selectUser);

   const status = useAppSelector(selectStatus);
   const { id } = useParams();

   const myCourses = useAppSelector(selectUserCourses);

   useEffect(() => {
      dispatch(getUserCoursesAsync());
      dispatch(getOneCourseAsync(id));
   }, [dispatch, id]);

   const tagColor = (name: string) => {
      const index = categoriesColors.findIndex((m) => m.name === name);
      if (index !== -1) {
         return categoriesColors[index].color;
      }
      return '';
   };

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

   const subStatus = (): boolean => {
      const result = myCourses.find((m) => m.id === id);
      if (result) {
         return false;
      }
      return true;
   };

   return (
      <Row>
         <Col span={12} offset={6}>
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
                  subStatus() && <DatePicker onChange={handleOnDate} />,
                  subStatus() ? (
                     <Button onClick={handleOnSubscribe} key="1">
                        <BellOutlined /> Subscribe
                     </Button>
                  ) : (
                     <Button onClick={handleOnUnsubscribe} key="1">
                        <BellOutlined /> Unsubscribe
                     </Button>
                  ),
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
