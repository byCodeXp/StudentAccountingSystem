/// <reference types="react-scripts" />

interface IRegisterRequest {
   firstName: string;
   lastName: string;
   age: number;
   email: string;
   password: string;   
}

interface ILoginRequest {
   email: string;
   password: string;
   remember: boolean;
}

interface IUser {
   id: string;
   firstName: string;
   lastName: string;
   age: number;
   email: string;
   role: string;
   registerAt: string;
   courses: [];
}

interface IdentityState {
   user: IUser | undefined;
   courses: ICourse[];
   status: 'idle' | 'loading' | 'failed' | 'success' | 'signed';
   errorMessage: string;
}

type Sort = 'Relevance' | 'New' | 'Popular' | 'Alphabetically';

interface ICoursesRequest {
   search?: string;
   page: number;
   perPage: number;
   sortBy: Sort;
   categories: Array<string>;
}

interface ICourse {
   id: string;
   name: string;
   description: string;
   preview: string;
   categories: ICategory[];
}

interface ICourseState {
   currentCourse: ICourse | undefined;
   courses: ICourse[];
   totalCount: number;
   status: 'idle' | 'loading' | 'success' | 'deleted' | 'failed';
   errorMessage: string;
}

interface ICategory {
   id: string;
   name: string;
   color: string;
}

interface ICategoryState {
   categories: ICategory[];
   status: 'idle' | 'loading' | 'success' | 'failed';
   errorMessage: string;
}

interface IUserState {
   users: IUser[];
   status: 'idle' | 'loading' | 'success' | 'failed';
   errorMessage: string;
}