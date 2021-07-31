import { Row, Button, Result } from 'antd';
import Page from '../Page';

function RegisterPage() {
  return (
    <Page>
      <Row style={{ height: '100%' }}>
        <Result
          style={{ margin: 'auto' }}
          status="success"
          title="Successfully registration!"
          subTitle="Activation link was send on your email address: johndoe@gmail.com"
          extra={[
            <Button type="primary" key="console">
              Ok
            </Button>,
          ]}
        />
      </Row>
    </Page>
  );
}

export default RegisterPage;
