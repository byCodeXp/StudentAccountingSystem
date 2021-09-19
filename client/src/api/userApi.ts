import { createClient, responseData, responseError } from './apiService';

const client = createClient('user');

const fetchAll = async (request: { page: number, perPage: number }) => client.get('/get', {
   params: request
}).then(responseData).catch(responseError);

const fetchSubscribe = async (request: { courseId: string; date: string; }) => client.post('/subscribe', request).then(responseData).catch(responseError);

const fetchUnsubscribe = async (courseId: string) => client.delete(`/unsubscribe/${courseId}`).then(responseData).catch(responseError);

const fetchUserCourses = async () => await client.get('/courses').then(responseData).catch(responseError);

const userApi = { fetchAll, fetchSubscribe, fetchUnsubscribe, fetchUserCourses };

export default userApi;