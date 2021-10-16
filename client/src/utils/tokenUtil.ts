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

const user = (): IUser | undefined => {
   const token = getToken();
   if (token) {
      const user = jwt.decode(token);
      if (user) {
         return {
            id: user.id,
            firstName: user.firstName,
            lastName: user.lastName,
            birthDay: user.birthDay,
            email: user.email,
            role: user.role,
            registerAt: '',
            courses: [],
         }
      }
   }
   return undefined;
}

export const tokenUtil = { getToken, setToken, clear, expired, bearer, user };
