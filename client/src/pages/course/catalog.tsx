import { useEffect, useState } from 'react';
import { Row, Col, Pagination, Card, Input, Space, Select, Collapse, Typography, List, Checkbox, } from 'antd';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { getCoursesAsync, selectCourses, selectCount, selectStatus, } from '../../features/courseSlice';
import { Link, useParams, useNavigate } from 'react-router-dom';
import { AppstoreOutlined } from '@ant-design/icons';
import { getCategoriesAsync, selectCategories, } from '../../features/categorySlice';

const { Meta } = Card;
const { Option } = Select;

export const CatalogPage = () => {
   const dispatch = useAppDispatch();
   const navigate = useNavigate();

   const categories = useAppSelector(selectCategories);

   const courses = useAppSelector(selectCourses);
   const totalCount = useAppSelector(selectCount);

   const status = useAppSelector(selectStatus);

   const { page } = useParams();
   const [sort, setSort] = useState<Sort>('Relevance');

   const [tags, setTags] = useState<Array<string>>([]);

   useEffect(() => {
      dispatch(getCategoriesAsync());
   }, [dispatch]);

   useEffect(() => {
      dispatch(
         getCoursesAsync({
            page: parseInt(page),
            perPage: 8,
            sortBy: sort,
            categories: tags,
         })
      );
   }, [dispatch, page, sort, tags]);

   const navigateToPage = (page: number) => {
      navigate(`/catalog/${page}`);
   };

   const handleChangeSort = (value: Sort) => {
      setSort(value);
   };

   const handleChangeCheck = (category: string, check: boolean) => {
      const index = tags.findIndex((m) => m === category);

      if (index !== -1) {
         if (check === false) {
            setTags([...tags.slice(0, index), ...tags.slice(index + 1)]);
         }
      } else {
         if (check === true) {
            setTags([...tags, category]);
         }
      }
   };

   return (
      <Row gutter={32}>
         <Col span={20}>
            <Input
               placeholder="Type to find here.."
               addonAfter={
                  <Space size={20}>
                     <span>Sort by:</span>
                     <Select
                        value={sort}
                        onChange={handleChangeSort}
                        className="select-after"
                        style={{ textAlign: 'left', width: 140 }}
                     >
                        <Option value="Relevance">Relevance</Option>
                        <Option value="New">New</Option>
                        <Option value="Popular">Popular</Option>
                        <Option value="Alphabetically">Alphabetically</Option>
                     </Select>
                  </Space>
               }
            />
            <Row gutter={24} style={{ marginTop: 24 }}>
               {courses.map((course, index) => (
                  <Col key={index} span={6}>
                     <Link to={`/details/${course.id}`}>
                        <Card
                           loading={status === 'loading'}
                           style={{ width: '100%', marginBottom: 24 }}
                           hoverable
                           cover={
                              <img
                                 style={{ height: 200, objectFit: 'cover' }}
                                 alt={course.name}
                                 src={
                                    course.preview ??
                                    'https://cdn.dribbble.com/users/1753953/screenshots/3818675/animasi-emptystate.gif'
                                 }
                              />
                           }
                        >
                           <Meta
                              title={course.name}
                              description={`${course.description.slice(0, 80)} ...`}
                           />
                        </Card>
                     </Link>
                  </Col>
               ))}
            </Row>
            <Col span={24}>
               <Row justify="center">
                  <Pagination
                     current={parseInt(page ?? '1')}
                     onChange={navigateToPage}
                     defaultPageSize={8}
                     total={totalCount}
                     style={{ marginTop: 24 }}
                  />
               </Row>
            </Col>
         </Col>
         <Col span={4}>
            <Collapse defaultActiveKey={['1']}>
               <Collapse.Panel
                  header={
                     <Space>
                        <AppstoreOutlined />
                        <Typography.Text>Technologies</Typography.Text>
                     </Space>
                  }
                  key="1"
               >
                  <List>
                     {categories.map((category, index) => (
                        <List.Item key={index}>
                           <Checkbox
                              onChange={(e) =>
                                 handleChangeCheck(
                                    category.name,
                                    e.target.checked
                                 )
                              }
                           >
                              {category.name}
                           </Checkbox>
                        </List.Item>
                     ))}
                  </List>
               </Collapse.Panel>
            </Collapse>
         </Col>
      </Row>
   );
};
