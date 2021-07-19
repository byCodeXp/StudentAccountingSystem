import './Header.css';
import { Layout, Button } from 'antd';
const { Header } = Layout;

function AppHeader() {
  return (
    <Header>
      <span style={{ color: '#ffffff' }}>Student Accounting System</span>
      <div>
        <Button href="login" type="link">
          Login
        </Button>
        <span style={{ color: '#c2c2c2' }}>|</span>
        <Button href="register" type="link">
          Register
        </Button>
      </div>
    </Header>
  );
}

export default AppHeader;
