import { createClient, responseData, responseError } from '../api/apiService';

const makeClient = () => {
   return createClient('course');
}

const fetchAll = async (request: ICoursesRequest) =>
   await makeClient().post('/get', request).then(responseData).catch(responseError);

const fetchOne = async (id: string) =>
   await makeClient().get(`/${id}`).then(responseData).catch(responseError);

const fetchAdd = async (course: ICourse) =>
   await makeClient().post('/create', course).then(responseData).catch(responseError);

const fetchUpdate = async (course: ICourse) =>
   await makeClient().put('/update', course).then(responseData).catch(responseError);

const fetchDelete = async (id: string) =>
   await makeClient().delete(`/delete/${id}`).then(responseData).catch(responseError);

const courseApi = { fetchAll, fetchOne, fetchAdd, fetchUpdate, fetchDelete };

export default courseApi;