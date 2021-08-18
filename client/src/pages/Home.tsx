import React from 'react';
import { Carousel, Row, Col, Divider, Card } from 'antd';
import { courses } from './MockData';

const { Meta } = Card;

const mockData = [
   {
      preview: 'https://i.ytimg.com/vi/SXtS4pccFfs/maxresdefault.jpg',
   },
   {
      preview:
         'https://app.softserveinc.com/apply/add/img/devops-crash-course-2.png?upd=2020-03-16%2021:19:35',
   },
   {
      preview:
         'https://assets.website-files.com/5fbd9df89f668234dc47843b/5fbe80df178b6cb1606dcdff_OG-IMAGE.png',
   },
];

const Home = () => {
   return (
      <Row justify="center">
         <Col span={12}>
            <Carousel>
               {mockData.map((item, index) => (
                  <div>
                     <img
                        key={index}
                        className="imageCarousel"
                        src={item.preview}
                     />
                  </div>
               ))}
            </Carousel>
            <Divider orientation="left" plain>
               Popular
            </Divider>
            <Row
               style={{ marginTop: 24 }}
               gutter={24}
               data-packery='{ "originLeft: false" }'
            >
               {courses.map((course, index) => (
                  <Col span={6}>
                     <Card
                        key={index}
                        style={{ width: '100%', marginBottom: 24 }}
                        hoverable
                        cover={<img alt={course.title} src={course.preview} />}
                     >
                        <Meta
                           title={course.title}
                           description={course.description}
                        />
                     </Card>
                  </Col>
               ))}
            </Row>
            <Divider orientation="left" plain>
               Trending
            </Divider>
            <Row
               style={{ marginTop: 24 }}
               gutter={24}
               data-packery='{ "originLeft: false" }'
            >
               {courses.reverse().map((course, index) => (
                  <Col span={6}>
                     <Card
                        key={index}
                        style={{ width: '100%', marginBottom: 24 }}
                        hoverable
                        cover={<img alt={course.title} src={course.preview} />}
                     >
                        <Meta
                           title={course.title}
                           description={course.description}
                        />
                     </Card>
                  </Col>
               ))}
            </Row>
         </Col>
      </Row>
   );
};

export default Home;
