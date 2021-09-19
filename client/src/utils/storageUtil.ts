const jwt = require('jsonwebtoken');

const TOKEN_NAME = 'access_token';
const COURSES_NAME = 'courses';

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
   const exp = jwt.decode(token).exp;
   if (exp < new Date().getTime() / 1000) {
      return true;
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
      return {
         id: user.id,
         firstName: user.firstName,
         lastName: user.lastName,
         age: user.age,
         email: user.email,
         role: user.role,
         registerAt: '', // TODO: Something
         courses: [],
      }
   }
   return undefined;
}

const setCourses = (courses: ICourse[]) => {
   localStorage.setItem(COURSES_NAME, JSON.stringify(courses));
}

const getCourses = () => {
   const data = localStorage.getItem(COURSES_NAME);
   if (data) {
      return JSON.parse(data);
   }
   return null;
}

const storageUtil = { getToken, setToken, clear, expired, bearer, user, getCourses, setCourses };

export default storageUtil;