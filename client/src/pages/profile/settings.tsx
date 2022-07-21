import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { Container } from '../../components/container';
import { selectStatus, selectUser, updateUserProfile } from '../../features/identitySlice';
import { Input, Form, Typography, Card, Button, PageHeader, Row, Spin, DatePicker } from 'antd';
import { LoadingOutlined } from '@ant-design/icons';
import identityApi from '../../api/identityApi';
import { useForm } from 'antd/lib/form/Form';
import moment from 'moment';

export const ProfileSettingsPage = () => {
   const dispatch = useAppDispatch();

   const user = useAppSelector(selectUser);
   const status = useAppSelector(selectStatus);

   const navigate = useNavigate();

   const [form] = useForm();

   const [isEnabled, setEnabled] = useState(false);
   const [loading, setLoading] = useState(false);

   const handleOnFinish = (values: IChangeProfileRequest) => {
      dispatch(updateUserProfile(values));
      setEnabled(false);
   };

   const handleOnPassowrdChange = async (values: IChangePassowrdRequest) => {
      setLoading(true);
      await identityApi.fetchChangePassword(values)
      .then(response => {
         setLoading(false);
      })
      .catch(error => {
         setLoading(false);
      });
   }

   const Title = () => {

      if (status === 'loading' || loading) {
         return (
            <Row justify="space-between">
               <Typography>Settings</Typography>
               <Spin indicator={<LoadingOutlined />} />
            </Row>
         );
      }

      return (
         <Row justify="space-between" align="middle">
            <Typography>Personal information</Typography>
            {isEnabled === true ? (
               <div>
                  <Button type="link" onClick={() => setEnabled(false)}>
                     Cancel
                  </Button>
                  <Button type="primary" htmlType="submit">
                     Confirm
                  </Button>
               </div>
            ) : (
               <Button type="ghost" onClick={() => setEnabled(true)}>
                  Edit
               </Button>
            )}
         </Row>
      );
   };

   useEffect(() => {
      form.setFieldsValue({ firstName: user?.firstName, lastName: user?.lastName, birthDay: moment(user?.birthDay) });
   }, [form, user]);

   return (
      <Container>
         <PageHeader title="Settings" onBack={() => navigate('/profile')}>
            <Form form={form} onFinish={handleOnFinish}>
               <Card title={<Title />}>
                  <Typography>First Name</Typography>
                  <Form.Item name="firstName">
                     <Input disabled={!isEnabled} />
                  </Form.Item>

                  <Typography>Last Name</Typography>
                  <Form.Item name="lastName">
                     <Input disabled={!isEnabled} />
                  </Form.Item>

                  <Typography>Birth day</Typography>
                  <Form.Item name="birthDay" rules={[
                  {
                     required: true,
                     message: 'Please input your birth day!',
                  },
               ]}>
                     <DatePicker style={{ width: '100%' }} disabled={!isEnabled} />
                  </Form.Item>
               </Card>
            </Form>
            <br />
            <Card title="Change password">
               <Form onFinish={handleOnPassowrdChange}> 
                  <Typography>Enter old password</Typography>
                  <Form.Item name="oldPassword">
                     <Input />
                  </Form.Item>
                  <Typography>Enter new password</Typography>
                  <Form.Item name="newPassword">
                     <Input />
                  </Form.Item>
                  <Button
                     type="primary"
                     style={{ float: 'right' }}
                     htmlType="submit"
                  >
                     Update
                  </Button>
               </Form>
            </Card>
         </PageHeader>
      </Container>
   );
};
