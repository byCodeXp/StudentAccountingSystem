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
   firstName: string;
   lastName: string;
   email: string;
}

interface IUserState {
   user: IUser;
   status: 'idle' | 'loading' | 'signed' | 'success' | 'error';
   error: string;
}
