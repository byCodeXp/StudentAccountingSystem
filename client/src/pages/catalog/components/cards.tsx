import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { Card, Col, Skeleton } from 'antd';
import {
   fetchCourses,
   selectCourses,
   selectPage,
   selectSort,
   selectTags,
   selectSearch,
   selectStatus,
} from '../../../features/catalogSlice';

const { Meta } = Card;

export const Cards = () => {
   const dispatch = useAppDispatch();

   const courses = useAppSelector(selectCourses);
   const tags = useAppSelector(selectTags);
   const sort = useAppSelector(selectSort);
   const page = useAppSelector(selectPage);
   const search = useAppSelector(selectSearch);
   const status = useAppSelector(selectStatus);

   useEffect(() => {
      dispatch(
         fetchCourses({
            search: search,
            categories: tags,
            sortBy: sort,
            perPage: 8,
            page: page,
         })
      );
   }, [dispatch, sort, tags, page, search]);

   return (
      <>
         {status === 'loading'
            ? [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map((item) => (
                 <Col
                    key={item}
                    xxl={{ span: 6 }}
                    xl={{ span: 8 }}
                    lg={{ span: 12 }}
                    xs={{ span: 24 }}
                 >
                    <Skeleton loading>
                       <Card style={{ width: '100%', marginBottom: 24 }}>
                          <Meta title="Title" description="Description" />
                       </Card>
                    </Skeleton>
                 </Col>
              ))
            : courses.map((course, index) => (
                 <Col
                    key={index}
                    xxl={{ span: 6 }}
                    xl={{ span: 8 }}
                    lg={{ span: 12 }}
                    xs={{ span: 24 }}
                 >
                    <Link to={`/details/${course.id}`}>
                       <Card
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
                             description={`${course.description.slice(
                                0,
                                80
                             )} ...`}
                          />
                       </Card>
                    </Link>
                 </Col>
              ))}
      </>
   );
};
