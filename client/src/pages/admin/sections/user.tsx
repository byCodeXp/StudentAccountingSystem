import { useEffect, useState } from 'react';
import { Table } from 'antd';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { HeadRow } from '../components/headRow';
import { loadUsersAsync, selectUsers } from '../../../features/adminSlice';

export const UserSection = () => {
   const dispatch = useAppDispatch();

   const [search, setSearch] = useState('');

   const users = useAppSelector(selectUsers);

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
               total: 100,
               showSizeChanger: true,
               pageSizeOptions: ['4', '8', '16', '32'],
               defaultPageSize: 4,
            }}
            dataSource={users}
            expandable={{
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
                  },
               },
            ]}
         />
      </>
   );
};
