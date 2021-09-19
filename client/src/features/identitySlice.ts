import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import identityApi from '../api/identityApi';
import userApi from '../api/userApi';
import { RootState } from '../app/store';
import storageUtil from '../utils/storageUtil';

const initialState: IdentityState = { 
   user: undefined,
   courses: [],
   status: 'idle',
   errorMessage: '',
};

export const loginAsync = createAsyncThunk(
   'identity/fetchLogin',
   async (request: ILoginRequest) => {
      const response = await identityApi.fetchLogin(request);
      storageUtil.setToken(response.token);
      return storageUtil.user();
   }
);

export const registerAsync = createAsyncThunk(
   'identity/fetchRegister',
   async (request: IRegisterRequest) => {
      return await identityApi.fetchRegister(request);
   }
)

export const getUserCoursesAsync = createAsyncThunk('account/fetchUserCourses', async () => {
   return await userApi.fetchUserCourses();
});

export const subscribeOnCourseAsync = createAsyncThunk('user/fetchSubscribe', async (request: { course: ICourse, date: string; }) => {
   await userApi.fetchSubscribe({ courseId: request.course.id, date: request.date });
   return request.course;
});

export const unsubscribeCourseAsync = createAsyncThunk('user/fetchUnsubscribe', async (course: ICourse) => {
   await userApi.fetchUnsubscribe(course.id);
   return course;
});

const identitySlice = createSlice({ // Rename to - accountSlice
   name: 'identity',
   initialState,
   reducers: {
      loadUser: (state) => {
         const token = storageUtil.getToken();
         if (token) {
            if (storageUtil.expired(token) === false) {
               const user = storageUtil.user();
               if (user) {
                  state.user = user;
                  state.status = 'signed';
               }
               const courses = storageUtil.getCourses();
               if (courses) {
                  state.courses = courses;
               }
            }
         }
      },
      logout: (state) => {
         state.status = 'idle';
         storageUtil.clear();
      },
      resetStatus: (state) => {
         state.status = 'idle';
      }
   },
   extraReducers: (builder) => {
      builder
         // LoginAsync
         .addCase(loginAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loginAsync.fulfilled, (state, action) => {
            state.user = action.payload;
            state.status = 'signed';
         })
         .addCase(loginAsync.rejected, (state, action) => {
            if (state.status === 'loading') {
               state.status = 'failed';
               state.errorMessage = action.error.message ?? 'Something went wrong, try again please !';
            }
         })
         // RegisterAsync
         .addCase(registerAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(registerAsync.fulfilled, (state) => {
            state.status = 'success';
         })
         .addCase(registerAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please !';
         })
         // Load courses
         .addCase(getUserCoursesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getUserCoursesAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.courses = action.payload;
         })
         .addCase(getUserCoursesAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please !';
         })
         // Subscribe
         .addCase(subscribeOnCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(subscribeOnCourseAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.courses = [...state.courses, action.payload];
         })
         .addCase(subscribeOnCourseAsync.rejected, (state) => {
            state.status = 'failed';
         })
         // Unsubscribe
         .addCase(unsubscribeCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(unsubscribeCourseAsync.fulfilled, (state, action) => {
            const index = state.courses.findIndex(e => e.id == action.payload.id);
            if (index !== -1) {
               state.courses = [...state.courses.slice(0, index), ...state.courses.slice(index + 1)];
            }
            state.status = 'success';
         })
         .addCase(unsubscribeCourseAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please !';
         })
  },
});

export default identitySlice.reducer;
export const { loadUser, logout, resetStatus } = identitySlice.actions;
export const selectUser = (state: RootState) => state.identity.user;
export const selectStatus = (state: RootState) => state.identity.status;
export const selectError = (state: RootState) => state.identity.errorMessage;
export const selectUserCourses = (state: RootState) => state.identity.courses;
