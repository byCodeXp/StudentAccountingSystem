import axios from 'axios';
import { SERVICE_UNAVAILABLE } from '../httpStatusCodes';

const API = 'https://localhost:5001/api/course';

export const fetchCourses = async (page: number) => {
   try {
      const response = await axios.get(`${API}/page/${page}`);
      return response.data;
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};

export const fetchCourse = async (id: string) => {
   try {
      const response = await axios.get(`${API}/details/${id}`);
      return response.data;
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};
