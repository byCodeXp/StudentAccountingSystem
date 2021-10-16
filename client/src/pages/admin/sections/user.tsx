import { useEffect, useState } from 'react';
import { Table } from 'antd';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { HeadRow } from '../components/headRow';
import { loadUsersAsync, selectUsers, selectUsersCount } from '../../../features/adminSlice';

export const UserSection = () => {
   const dispatch = useAppDispatch();

   const [search, setSearch] = useState('');

   const users = useAppSelector(selectUsers);
   const totalCount = useAppSelector(selectUsersCount);

   const handleOnSearch = (value: string) => {
      setSearch(value);
   }

   useEffect(() => {
      dispatch(loadUsersAsync({ search, page: 1, perPage: 4 }));
   }, [dispatch, search]);

   return (
      <>
         <HeadRow title="" onSearch={handleOnSearch} />
         <Table
            pagination={{
               total: totalCount,
               showSizeChanger: true,
               pageSizeOptions: ['4', '8', '16', '32'],
               defaultPageSize: 4,
            }}
            dataSource={users}
            expandable={{
               rowExpandable: (user) => user.courses.length > 0,
               expandedRowRender: (record) => (
                  <Table
                     dataSource={record.courses}
                     rowKey="id"
                     columns={[
                        {
                           title: 'Title',
                           dataIndex: 'name',
                           key: 'name',
                        },
                        {
                           title: 'Description',
                           dataIndex: 'description',
                           key: 'description',
                        },
                     ]}
                  />
               ),
            }}
            rowKey={(item) => item.id}
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
                  title: 'Birth day',
                  dataIndex: 'birthDay',
                  key: 'birthDay',
                  render: (item) => {
                     const date = new Date(item);
                     return date.toDateString();
                  }
               },
               {
                  title: 'Registered date',
                  dataIndex: 'registerAt',
                  key: 'registerAt',
                  render: (item) => {
                     const date = new Date(item);
                     return date.toDateString();
                  },
               },
            ]}
         />
      </>
   );
};
