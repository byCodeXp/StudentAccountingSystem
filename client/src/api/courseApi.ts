import { createClient, responseData, responseError } from './apiService';

const client = createClient('course');

const fetchCourses = async (request: ICoursesRequest) =>
   await client
      .get('get', {
         params: request,
      })
      .then(responseData)
      .catch(responseError);

const fetchOneCourse = async (id: string) =>
   await client.get(`/${id}`).then(responseData).catch(responseError);

const fetchAddCourse = async (course: ICourse) =>
   await client.post('/create', course).then(responseData).catch(responseError);

const fetchUpdateCourse = async (id: string, course: ICourse) =>
   await client.put(`/update/${id}`, course).then(responseData).catch(responseError);

const fetchDeleteCourse = async (id: string) =>
   await client.delete(`/delete/${id}`).then(responseData).catch(responseError);

const courseApi = {
   fetchCourses,
   fetchOneCourse,
   fetchAddCourse,
   fetchUpdateCourse,
   fetchDeleteCourse,
};

export default courseApi;
