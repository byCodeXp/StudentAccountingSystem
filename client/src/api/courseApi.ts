import { request } from 'http';
import { createClient, responseData, responseError } from '../api/apiService';
const qs = require('qs');

const client = createClient('course');

const fetchAll = async (request: ICoursesRequest) =>
   await client.post('/get', request).then(responseData).catch(responseError);

const fetchOne = async (id: string) =>
   await client.get(`/${id}`).then(responseData).catch(responseError);

const fetchAdd = async (course: ICourse) =>
   await client.post('/create', course).then(responseData).catch(responseError);

const fetchUpdate = async (course: ICourse) =>
   await client.put('/update', course).then(responseData).catch(responseError);

const fetchDelete = async (id: string) =>
   await client.delete(`/delete/${id}`).then(responseData).catch(responseError);

const courseApi = { fetchAll, fetchOne, fetchAdd, fetchUpdate, fetchDelete };

export default courseApi;