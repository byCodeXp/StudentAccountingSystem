import { createClient, responseData, responseError } from './apiService';

const client = createClient('user');

const fetchUsers = async (request: IUserRequest) =>
   await client
      .get('/get', {
         params: request,
      })
      .then(responseData)
      .catch(responseError);

const fetchOneUser = async (id: string) =>
   await client.get(`/${id}`).then(responseData).catch(responseError);

const userApi = {
   fetchUsers,
};

export default userApi;
