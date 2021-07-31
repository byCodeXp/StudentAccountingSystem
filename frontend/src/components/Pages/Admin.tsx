import { Layout, Menu } from 'antd';
import {
  BulbOutlined,
  AreaChartOutlined,
  DatabaseOutlined,
  UserOutlined,
  DashboardOutlined
} from '@ant-design/icons';

const { Header, Content, Footer, Sider } = Layout;
const { SubMenu } = Menu;

function AdminPage() {
  return (
    <Layout>
      <Sider
        style={{
          overflow: 'auto',
          height: '100vh',
          position: 'fixed',
          left: 0,
        }}
      >
        <div className="logo" />
        <Menu theme="dark" mode="inline" defaultSelectedKeys={['4']}>
          <Menu.Item key="1" icon={<DashboardOutlined />}>
            Dashboard
          </Menu.Item>
          <SubMenu key="sub1" icon={<UserOutlined />} title="Users">
            <Menu.Item key="2">All</Menu.Item>
            <Menu.Item key="3">Table view</Menu.Item>
          </SubMenu>
          <SubMenu key="sub2" icon={<DatabaseOutlined />} title="Courses">
            <Menu.Item key="4">All</Menu.Item>
            <Menu.Item key="5">Table view</Menu.Item>
          </SubMenu>
          <SubMenu key="sub3" icon={<AreaChartOutlined />} title="Statistics">
            <Menu.Item key="6">Data</Menu.Item>
            <Menu.Item key="7">Reports</Menu.Item>
          </SubMenu>
        </Menu>
      </Sider>
      <Layout className="site-layout" style={{ marginLeft: 200 }}>
        <Header className="site-layout-background" style={{ padding: '0 32 px' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', width: '100%' }}>
            <span>Dashboard</span>
            <BulbOutlined />
          </div>
        </Header>
        <Content style={{ margin: '24px 16px 0', overflow: 'initial' }}>
          <div className="site-layout-background" style={{ padding: 24, textAlign: 'center' }}>

          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
      </Layout>
    </Layout>
  );
}

export default AdminPage;
