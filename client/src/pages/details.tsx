import { useEffect, useState } from 'react';
import { PageHeader, Row, Col, Tag, Button, DatePicker } from 'antd';
import { useParams } from 'react-router-dom';
import { BellOutlined } from '@ant-design/icons';
import moment, { now } from 'moment';
import courseApi from '../api/courseApi';
import userApi from '../api/userApi';

export const DetailsPage = () => {
   const { id } = useParams();

   const [course, setCourse] = useState<ICourse | undefined>();

   const [date, setDate] = useState('');

   const handleOnDate = (date: any, dateString: any) => {
      setDate(dateString);
   };

   const handleOnSubscribe = () => {
      if (course?.subscribed) {
         userApi.fetchUnsubscribe(id).then(() => {
            courseApi.fetchOne(id).then(response => {
               setCourse(response);
            });
         });
      }
      else {
         userApi.fetchSubscribe({ courseId: id, date: date }).then(() => {
            courseApi.fetchOne(id).then(response => {
               setCourse(response);
            });
         });
      }
   }

   useEffect(() => {
      courseApi.fetchOne(id).then(response => {
         setCourse(response);
      })
   }, [id]);

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
                  !course?.subscribed && <DatePicker key="1" onChange={handleOnDate} disabledDate={(currentDate: moment.Moment) => currentDate < moment(now())}/>,
                  <Button key="2" onClick={handleOnSubscribe}>
                     <BellOutlined /> {course?.subscribed ? 'Unsubscribe' : 'Subscribe'}
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
