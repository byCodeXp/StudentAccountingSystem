import React from 'react';
import { useAppSelector } from '../../../app/hooks';
import { selectPage, selectTotalCount } from '../../../features/catalogSlice';
import { useNavigate } from 'react-router';
import { Pagination as AntdPagination } from 'antd';

export const Pagination = () => {

   const navigate = useNavigate();

   const page = useAppSelector(selectPage);
   const totalCount = useAppSelector(selectTotalCount);

   const navigateToPage = (page: number) => {
      navigate(`/catalog/${page}`);
   };

   return (
      <AntdPagination
         current={page}
         onChange={navigateToPage}
         defaultPageSize={8}
         total={totalCount}
         style={{ marginTop: 24 }}
      />
   );
};
