const jwt = require('jsonwebtoken');

const tokenName = 'access_token';

const get = () => {
   const token = localStorage.getItem(tokenName);
   if (token) {
      return token;
   }
   return null;
};

const set = (token: string) => {
   localStorage.setItem(tokenName, token);
};

const clear = () => {
   localStorage.removeItem(tokenName);
};

const user = (token: string = '') => {
   if (token === '') {
      return jwt.decode(get());
   }
   return jwt.decode(token);
};

const expired = (token: string = '') => {
   if (token === '') {
      const t = get();
      if (t) {
         const data = jwt.decode(t);
         return data.exp < new Date().getTime() / 1000;
      }
   }
   const data = jwt.decode(token);
   return data.exp < new Date().getTime() / 1000;
};

export const tokenUtil = {
   get,
   set,
   clear,
   user,
};
