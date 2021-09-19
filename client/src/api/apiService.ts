import axios from 'axios';
import tokenUtil from '../utils/storageUtil';

const BASE_ADDRESS = 'https://localhost:5001/api';

export const createClient = (route: string) => {
   return axios.create({
      baseURL: `${BASE_ADDRESS}/${route}`,
      timeout: 1000,
      headers: {
         Authorization: tokenUtil.bearer(),
      },
   });
};

export const responseData = (response: any) => {
   return response.data;
};


export const responseError = (error: any) => {
   throw new Error(error.response.data.Message);
};