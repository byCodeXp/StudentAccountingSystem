import axios from 'axios';
import { SERVICE_UNAVAILABLE } from '../httpStatusCodes';

const API = 'https://localhost:5001/api';

export const fetchCourses = async (page: number) => {
   try {
      const response = await axios.get(`${API}/course/page/${page}`);
      return response.data;
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};

export const fetchUsers = async (page: number) => {
   try {
      const token = localStorage.getItem('access_token');
      const response = await axios.get(`${API}/user/page/${page}`, {
         headers: {
            Authorization: `Bearer ${token}`,
         },
      });
      return response.data;
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};
