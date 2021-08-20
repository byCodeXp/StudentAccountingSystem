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
   role: string;
}

interface IUserState {
   user: IUser;
   status: 'idle' | 'loading' | 'signed' | 'success' | 'error';
   error: string;
}

interface ICourse {
   id: string;
   title: string;
   description: string;
   preview: string;
}

interface ICourseState {
   courses: ICourse[];
   currentCourse: ICourse;
   status: 'idle' | 'loading' | 'success' | 'error';
   error: string;
}

interface IAdminState {
   users: IUser[];
   courses: ICourse[];
   status: 'idle' | 'loading' | 'success' | 'error';
   error: string;
}
