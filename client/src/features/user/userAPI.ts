import axios from 'axios';
import { SERVICE_UNAVAILABLE } from '../httpStatusCodes';
const jwt = require('jsonwebtoken');

const API = 'https://localhost:5001/api/identity';

export const fetchLogin = async (request: ILoginRequest) => {
   await new Promise((resolve) => setTimeout(resolve, 500)); // TODO: Remove this delay
   try {
      const response = await axios.post(`${API}/login`, request);
      const token = response.data;

      localStorage.setItem('access_token', token);

      return jwt.decode(token);
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};

export const fetchRegister = async (request: IRegisterRequest) => {
   try {
      const response = await axios.post(`${API}/register`, request);
      return response.status;
   } catch (error) {
      if (error.response) {
         return error.response.status;
      }
      return SERVICE_UNAVAILABLE;
   }
};
