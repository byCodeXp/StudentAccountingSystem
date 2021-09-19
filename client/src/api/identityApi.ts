import { createClient, responseData, responseError } from './apiService';

const client = createClient('identity');

const fetchLogin = async (request: ILoginRequest) =>
   await client.post('/login', request).then(responseData).catch(responseError);

const fetchRegister = async (request: IRegisterRequest) => 
   await client.post('/register', request).then(responseData).catch(responseError);

const identityApi = { fetchLogin, fetchRegister }

export default identityApi;