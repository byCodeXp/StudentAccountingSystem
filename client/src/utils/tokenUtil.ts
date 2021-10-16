const jwt = require('jsonwebtoken');

const TOKEN_NAME = 'access_token';

const getToken = () => {
   const token = localStorage.getItem(TOKEN_NAME);
   if (token) {
      return token;
   }
   return undefined;
};

const setToken = (token: string) => {
   localStorage.setItem(TOKEN_NAME, token);
}

const clear = () => {
   localStorage.removeItem(TOKEN_NAME);
}

const expired = (token: string) => {
   const exp = jwt.decode(token);

   if (exp) {
      if (exp < new Date().getTime() / 1000) {
         return true;
      }
   }
   
   return false;
}

const bearer = () => {
   const token = getToken();
   if (token) {
      return `Bearer ${token}`;
   }
   return undefined;
}

export const tokenUtil = { getToken, setToken, clear, expired, bearer };
