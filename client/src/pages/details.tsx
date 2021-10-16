import { useEffect, useState } from 'react';
import {
   PageHeader,
   Row,
   Col,
   Tag,
   Button,
   DatePicker,
   Spin,
   Skeleton,
} from 'antd';
import { useParams } from 'react-router-dom';
import { BellOutlined, LoadingOutlined } from '@ant-design/icons';
import moment, { now } from 'moment';
import courseApi from '../api/courseApi';
import userApi from '../api/userApi';
import { Container } from '../components/container';
import { selectUser } from '../features/identitySlice';
import { useAppSelector } from '../app/hooks';

const SubscribeComponent = (props: {
   loading: boolean;
   subscribed: boolean;
   onClick: any;
   onDate: any;
}) => {
   if (props.loading === true) {
      return <Spin indicator={<LoadingOutlined />} />;
   }
   return (
      <>
         {props.subscribed !== true && (
            <DatePicker
               key="1"
               onChange={(date) => props.onDate(date?.toISOString())}
               disabledDate={(currentDate: moment.Moment) =>
                  currentDate < moment(now())
               }
            />
         )}
         <Button key="2" onClick={() => props.onClick()}>
            <BellOutlined /> {props.subscribed ? 'Unsubscribe' : 'Subscribe'}
         </Button>
      </>
   );
};

export const DetailsPage = () => {
   const { id } = useParams();

   const [course, setCourse] = useState<ICourse | undefined>();
   const [date, setDate] = useState('');
   const [loading, setLoading] = useState(false);

   const user = useAppSelector(selectUser);

   const handleOnDate = (date: string) => {
      setDate(date);
   };

   const handleOnSubscribe = () => {
      console.log(date);
      setLoading(true);
      if (course?.subscribed) {
         userApi.fetchUnsubscribe(id).then(() => {
            courseApi.fetchOne(id).then((response) => {
               setCourse(response);
               setLoading(false);
            });
         });
      } else {
         userApi.fetchSubscribe({ courseId: id, date: date }).then(() => {
            courseApi.fetchOne(id).then((response) => {
               setCourse(response);
               setLoading(false);
            });
         });
      }
   };

   useEffect(() => {
      setLoading(true);
      courseApi.fetchOne(id).then((response) => {
         setCourse(response);
         setLoading(false);
      });
   }, [id]);

   return (
      <Container>
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
               user && (
                  <SubscribeComponent
                     loading={loading}
                     subscribed={course?.subscribed === true}
                     onDate={handleOnDate}
                     onClick={handleOnSubscribe}
                  />
               ),
            ]}
         >
            <Row gutter={32}>
               <Col span={24}>
                  <Skeleton loading={loading} active={true}>
                     <img
                        style={{ width: '100%' }}
                        src={course?.preview}
                        alt={course?.name}
                     />
                     <p style={{ marginTop: 16 }}>{course?.description}</p>
                  </Skeleton>
               </Col>
            </Row>
         </PageHeader>
      </Container>
   );
};
