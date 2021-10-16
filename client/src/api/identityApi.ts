import { createClient, responseData, responseError } from './apiService';

const makeClient = () => {
   return createClient('identity');
}

const fetchLogin = async (request: ILoginRequest) =>
   await makeClient().post('/login', request).then(responseData).catch(responseError);

const fetchRegister = async (request: IRegisterRequest) => 
   await makeClient().post('/register', request).then(responseData).catch(responseError);

const fetchUpdateProfile = async (request: IChangeProfileRequest) =>
   await makeClient().put('/update-personal-data', request).then(responseData).catch(responseError);

const fetchChangePassword = async (request: IChangePassowrdRequest) =>
   await makeClient().put('/change-password', request).then(responseData).catch(responseError);

const fetchFacebookLogin = async (request: { userId: string; token: string }) =>
   await makeClient().post(`/facebook-login`, request).then(responseData).catch(responseError);

const identityApi = { fetchLogin, fetchRegister, fetchUpdateProfile, fetchChangePassword, fetchFacebookLogin }

export default identityApi;