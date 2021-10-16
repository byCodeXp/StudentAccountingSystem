import { createClient, responseData, responseError } from './apiService';

const makeClient = () => {
   return createClient('user');
};

const fetchAll = async (request: { page: number; perPage: number }) =>
   makeClient()
      .get('/get', {
         params: request,
      })
      .then(responseData)
      .catch(responseError);

const fetchSubscribe = async (request: { courseId: string; date: string }) =>
   makeClient()
      .post('/subscribe', request)
      .then(responseData)
      .catch(responseError);

const fetchUnsubscribe = async (courseId: string) =>
   makeClient()
      .delete(`/unsubscribe/${courseId}`)
      .then(responseData)
      .catch(responseError);

const fetchUserCourses = async () =>
   await makeClient().get('/courses').then(responseData).catch(responseError);

const userApi = {
   fetchAll,
   fetchSubscribe,
   fetchUnsubscribe,
   fetchUserCourses,
};

export default userApi;
