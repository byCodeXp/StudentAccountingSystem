import { useEffect } from 'react';
import { Row, Col } from 'antd';
import { useAppDispatch } from '../../app/hooks';
import { useParams } from 'react-router-dom';
import { setPage, setSearch, setTags } from '../../features/catalogSlice';
import { Tags } from './components/tags';
import { Cards } from './components/cards';
import { Pagination } from './components/pagination';
import { SearchAndSort } from './components/searchAndSort';

export const CatalogPage = () => {
   
   const dispatch = useAppDispatch();

   const { page } = useParams();
   useEffect(() => {
      dispatch(setPage(page));
      dispatch(setTags([]));
      dispatch(setSearch(''));
   }, [dispatch, page]);

   return (
      <Row gutter={32}>
         <Col xxl={{ span: 20 }} xl={{ span: 19 }} lg={{ span: 18 }}>
            <SearchAndSort />
            <Row gutter={24} style={{ marginTop: 24 }}>
               <Cards />
            </Row>
            <Col span={24}>
               <Row justify="center">
                  <Pagination />
               </Row>
            </Col>
         </Col>
         <Col xxl={{ span: 4 }} xl={{ span: 5 }} lg={{ span: 6 }}>
            <Tags />
         </Col>
      </Row>
   );
};
