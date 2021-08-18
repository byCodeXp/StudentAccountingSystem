import React from 'react';
import { Card } from 'antd';
const { Meta } = Card;

function CatalogCard(props: {
   key: number;
   course: {
      title: string;
      preview: string;
      description: string;
   };
}) {
   const { title, preview, description } = props.course;

   return (
      <Card
         key={props.key}
         style={{ width: '100%', marginBottom: 24 }}
         hoverable
         cover={<img alt={title} src={preview} />}
      >
         <Meta title={title} description={description} />
      </Card>
   );
}

export default CatalogCard;
