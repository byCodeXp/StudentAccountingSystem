import { useEffect } from 'react';
import { UserOutlined, SettingOutlined } from '@ant-design/icons';
import {
   Row,
   Col,
   Space,
   Avatar,
   Typography,
   Button,
   Divider,
   Card,
   Image,
   Skeleton,
} from 'antd';
import { Link } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import {
   getUserCoursesAsync,
   selectStatus,
   selectUser,
   selectUserCourses,
} from '../../features/identitySlice';
import { Container } from '../../components/container';

export const ProfilePage = () => {
   const dispatch = useAppDispatch();

   const user = useAppSelector(selectUser);
   const courses = useAppSelector(selectUserCourses);
   const status = useAppSelector(selectStatus);

   useEffect(() => {
      dispatch(getUserCoursesAsync());
   }, [dispatch]);

   return (
      <Container>
         <Row justify="space-between" align="middle">
            <Space>
               <Avatar shape="square" size={48} icon={<UserOutlined />} />
               <Typography.Text>{`${user?.firstName} ${user?.lastName}`}</Typography.Text>
            </Space>
            <Link to="settings">
               <Button shape="circle" icon={<SettingOutlined />} />
            </Link>
         </Row>
         <Divider />
         <Row>
            {status === 'loading'
               ? [1, 2, 3, 4, 5].map((item) => <Skeleton key={item} />)
               : courses.map((course, index) => (
                    <Col span={24} key={index} style={{ marginBottom: 32 }}>
                       <Card cover={<Image src={course.preview} />}>
                          <h2>{course.name}</h2>
                       </Card>
                    </Col>
                 ))}
         </Row>
      </Container>
   );
};
