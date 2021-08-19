import { useState, createElement } from 'react';
import { PageHeader, Tabs, Button, Row, Col, Tag, Form } from 'antd';
import { Steps, Tooltip } from 'antd';
import { Comment, Avatar, Input } from 'antd';
import { DislikeFilled, DislikeOutlined, LikeFilled, LikeOutlined, BellOutlined } from '@ant-design/icons';

const { TextArea } = Input;

const { Step } = Steps;

const { TabPane } = Tabs;

const Editor = () => <></>;

const Course = () => {
   const [likes, setLikes] = useState(0);
   const [dislikes, setDislikes] = useState(0);
   const [action, setAction] = useState('');

   const like = () => {
      setLikes(1);
      setDislikes(0);
      setAction('liked');
   };

   const dislike = () => {
      setLikes(0);
      setDislikes(1);
      setAction('disliked');
   };

   const actions = [
      <Tooltip key="comment-basic-like" title="Like">
         <span onClick={like}>
            {createElement(action === 'liked' ? LikeFilled : LikeOutlined)}
            <span className="comment-action">{likes}</span>
         </span>
      </Tooltip>,
      <Tooltip key="comment-basic-dislike" title="Dislike">
         <span onClick={dislike}>
            {createElement(action === 'disliked' ? DislikeFilled : DislikeOutlined)}
            <span className="comment-action">{dislikes}</span>
         </span>
      </Tooltip>,
      <span key="comment-basic-reply-to">Reply to</span>,
   ];

   return (
      <PageHeader
         className="site-page-header-responsive"
         onBack={() => window.history.back()}
         title="DevOps: Crash Course. SoftServe | IT Academy"
         tags={[
            <Tag color="green">node.js</Tag>,
            <Tag color="purple">.net</Tag>,
            <Tag color="blue">c#</Tag>,
            <Tag color="orange">javaScript</Tag>,
         ]}
         extra={[
            <Button key="3">
               <BellOutlined /> Subscribe
            </Button>,
         ]}
      >
         <Row gutter={32}>
            <Col span={12}>
               <img style={{ width: '100%' }} src="https://app.softserveinc.com/apply/add/img/devops-crash-course-2.png" />
               <Tabs defaultActiveKey="1">
                  <TabPane style={{ padding: 8 }} tab="Description" key="1">
                     Lorem ipsum dolor sit amet, consectetur adipisicing elit. Accusamus aliquid debitis doloremque ducimus,
                     facilis illum impedit itaque labore magnam maiores neque praesentium, reiciendis sequi ullam voluptate.
                     Amet, cupiditate eaque itaque laborum nam vitae! Dolores ducimus eaque placeat saepe. Accusamus accusantium
                     architecto asperiores inventore nobis omnis quaerat quisquam quos reprehenderit voluptatum. A aliquid animi
                     aspernatur atque corporis culpa cumque debitis dolor doloremque ea et eum hic illo ipsam iusto laudantium
                     minima, molestiae nemo neque, odio officiis provident qui, reiciendis soluta temporibus voluptas voluptate.
                     Accusamus assumenda blanditiis consequatur consequuntur et libero modi nulla provident quaerat tempore.
                     Accusantium doloribus impedit nihil quasi vel.
                     <Steps progressDot current={1} direction="vertical">
                        <Step title="Finished" description="Introduction" />
                        <Step title="Finished" description="Create a services site 2015-09-01." />
                        <Step title="In Progress" description="Solve initial network problems 2015-09-01" />
                        <Step title="Waiting" description="Technical testing 2015-09-01" />
                        <Step title="Waiting" description="Network problems being solved 2015-09-01" />
                     </Steps>
                  </TabPane>
                  <TabPane tab="Details" key="2">
                     Details
                  </TabPane>
               </Tabs>
            </Col>
            <Col span={12}>
               Comments:
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  content={
                     <>
                        <Form.Item>
                           <TextArea showCount maxLength={256} placeholder="Left feedback..." rows={4} />
                        </Form.Item>
                        <Form.Item>
                           <Button htmlType="submit" type="primary">
                              Comment
                           </Button>
                        </Form.Item>
                     </>
                  }
               />
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  author="John Doe"
                  content="Good course! & Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis est iste porro quaerat
                     sit! Dolorem in laborum magni nobis qui, recusandae voluptatem. Iure, quo."
                  datetime={'12:00 AM'}
                  actions={actions}
               />
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  author="John Doe"
                  content="Good course! & Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis est iste porro quaerat
                     sit! Dolorem in laborum magni nobis qui, recusandae voluptatem. Iure, quo."
                  datetime={'12:00 AM'}
                  actions={actions}
               />
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  author="John Doe"
                  content="Good course! & Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis est iste porro quaerat
                     sit! Dolorem in laborum magni nobis qui, recusandae voluptatem. Iure, quo."
                  datetime={'12:00 AM'}
                  actions={actions}
               />
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  author="John Doe"
                  content="Good course! & Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis est iste porro quaerat
                     sit! Dolorem in laborum magni nobis qui, recusandae voluptatem. Iure, quo."
                  datetime={'12:00 AM'}
                  actions={actions}
               />
               <Comment
                  avatar={<Avatar src="https://static.wikia.nocookie.net/rustarwars/images/f/f4/HanSolo.jpg" alt="Han Solo" />}
                  author="John Doe"
                  content="Good course! & Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis est iste porro quaerat
                     sit! Dolorem in laborum magni nobis qui, recusandae voluptatem. Iure, quo."
                  datetime={'12:00 AM'}
                  actions={actions}
               />
            </Col>
         </Row>
      </PageHeader>
   );
};

export default Course;
