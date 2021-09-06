/// <reference types="react-scripts" />

interface ICategory {
   id: string;
   name: string;
}

interface ICourse {
   id: string | null;
   name: string;
   description: string;
   preview: string;
   categories: ICategory[];
}

enum SortBy {
   Relevance,
   New,
   Popular,
   Alphabetically,
}

interface ICoursesRequest {
   page: number;
   perPage: number;
   sortBy: SortBy;
   categories: string[];
}

interface ICourseState {
   courses: ICourse[];
   count: number;
   currentCourse: ICourse | null;
   status: 'idle' | 'loading' | 'success' | 'error';
   error: string;
}

interface ICategoryState {
   categories: ICategory[];
   status: 'idle' | 'loading' | 'success' | 'error';
   error: string;
}

interface IdentityState {
   user: IUser | null;
   status: 'guest' | 'loading' | 'signed' | 'failed' | 'success';
   error: string;
}

interface ILoginRequest {
   email: string;
   password: string;
   remember: boolean;
}

interface IRegisterRequest {
   firstName: string;
   lastName: string;
   age: number;
   email: string;
   password: string;
}

interface IUser {
   id: string;
   firstName: string;
   lastName: string;
   age: number;
   email: string;
   courses: ICourse[];
   role: string;
}

interface IUserRequest {
   page: number;
   perPage: number;
}

interface IUserState {
   users: IUser[];
   status: 'idle' | 'loading' | 'success' | 'error';
   error: string;
}
