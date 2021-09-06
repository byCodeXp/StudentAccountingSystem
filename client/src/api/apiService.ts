import axios from 'axios';
import { tokenUtil } from '../utils/tokenUtil';

export const createClient = (route: string) => {
   return axios.create({
      baseURL: `https://localhost:5001/api/${route}`,
      timeout: 1000,
      headers: {
         Authorization: `Bearer ${tokenUtil.get()}`,
      },
   });
};

export const responseData = (response: any) => response.data;

export const responseError = (error: any) => {
   throw new Error(error?.message ?? 'Something went wrong, please try again!');
};
