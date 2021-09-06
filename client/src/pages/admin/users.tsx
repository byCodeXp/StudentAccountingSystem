import React, { useEffect } from 'react';
import { Table } from 'antd';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { getUsersAsync, selectUsers } from '../../features/userSlice';

const Users = () => {
   const dispatch = useAppDispatch();

   const users = useAppSelector(selectUsers);

   const request = {
      page: 1,
      perPage: 1,
   };

   useEffect(() => {
      dispatch(getUsersAsync(request));
   }, []);

   return (
      <Table
         dataSource={users}
         columns={[
            { title: 'First name', dataIndex: 'firstName', key: 'firstName' },
            { title: 'Last Name', dataIndex: 'lastName', key: 'lastName' },
            { title: 'Age', dataIndex: 'age', key: 'age' },
            { title: 'Email', dataIndex: 'email', key: 'email' },
         ]}
      />
   );
};

export default Users;
