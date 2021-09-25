import axios, { AxiosError } from 'axios';
import storageUtil from '../utils/storageUtil';

const BASE_ADDRESS = 'https://localhost:5001/api';

export const createClient = (route: string) => {
   return axios.create({
      baseURL: `${BASE_ADDRESS}/${route}`,
      timeout: 1500,
      headers: {
         Authorization: storageUtil.bearer(),
      },
   });
};

export const responseData = (response: any) => {
   return response.data;
};


export const responseError = (error: AxiosError) => {
   console.log(error.code);
   console.log(error?.response);
   if (error.response?.data) {
      throw new Error(error.response?.data.Message);
   }

   if (error.code === 'ECONNABORTED') {
      throw new Error("Server Not Respond");
   }

   throw new Error("Internal Server Error");
};