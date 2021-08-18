import React from 'react';
import { Result, Button } from 'antd';
import { SmileOutlined } from '@ant-design/icons';

const Confirm = () => {
   return (
      <div className="place-middle">
         <Result
            icon={<SmileOutlined />}
            title="Great, confirm email before continue."
            subTitle="Activation link was send on your email address."
            extra={<Button type="primary">Done</Button>}
         />
      </div>
   );
};

export default Confirm;
