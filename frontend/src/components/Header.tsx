import { Link } from 'react-router-dom';
import { Header } from 'antd/es/layout/layout';

export default () => {
  return (
    <Header style={{ display: 'flex', justifyContent: 'space-between' }}>
      <Link to="/">
        <span style={{ color: 'white' }}>BrandName</span>
      </Link>
      <Link to="/login">Sign In</Link>
    </Header>
  );
};
