import { useEffect } from 'react';
import { Table } from 'antd';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { getUsersAsync, selectUsers } from '../../../features/userSlice';
import { HeadRow } from '../components/headRow';

const courseTable = () => {
   return <Table rowKey="courses" columns={[
      {
      }
   ]} />;
}

export const UserSection = () => {
   const dispatch = useAppDispatch();

   const users = useAppSelector(selectUsers)

   useEffect(() => {
      dispatch(getUsersAsync({ page: 1, perPage: 4 }))
   }, [dispatch])
   
   return (
      <>
         <HeadRow title="Users" />
         <Table
            pagination={{
               total: 100,
               showSizeChanger: true,
               pageSizeOptions: ['4', '8', '16', '32'],
               defaultPageSize: 4,
            }}
            dataSource={users}
            expandable={{
               expandedRowRender: (record) => (
                  <Table dataSource={record.courses} columns={[
                     {
                        title: 'Title',
                        dataIndex: 'name',
                        key: 'name'
                     },
                     {
                        title: 'Description',
                        dataIndex: 'description',
                        key: 'description'
                     },
                  ]}/>
               )
             }}
            columns={[
               {
                  title: 'First name',
                  dataIndex: 'firstName',
                  key: 'firstName',
               },
               {
                  title: 'Last name',
                  dataIndex: 'lastName',
                  key: 'lastName',
               },
               {
                  title: 'Age',
                  dataIndex: 'age',
                  key: 'age',
               },
               {
                  title: 'Registered date',
                  dataIndex: 'registerAt',
                  key: 'registerAt',
                  render: (item) => {
                     const date = new Date(item);
                     return date.toDateString();
                  }
               }
            ]}
         />
      </>
   );
};
