import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { fetchCourses, fetchUsers } from './adminAPI';
import { statusCodeText } from '../httpStatusCodes';

const initialState = {
   users: [],
   courses: [],
   status: 'idle',
   error: '',
};

export const getCoursesAsync = createAsyncThunk('admin/fetchCourses', async (page: number): Promise<{} | number> => {
   return await fetchCourses(page);
});

export const getUsersAsync = createAsyncThunk('admin/fetchUsers', async (page: number): Promise<{} | number> => {
   return await fetchUsers(page);
});

export const adminSlice = createSlice({
   name: 'course',
   initialState,
   reducers: {},
   extraReducers: (builder) => {
      builder
         .addCase(getCoursesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getCoursesAsync.fulfilled, (state, action) => {
            if (typeof action.payload === 'number') {
               state.status = 'error';
               state.error = statusCodeText(action.payload);
            } else {
               state.courses = action.payload as [];
               state.status = 'success';
            }
         })
         .addCase(getUsersAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getUsersAsync.fulfilled, (state, action) => {
            if (typeof action.payload === 'number') {
               state.status = 'error';
               state.error = statusCodeText(action.payload);
            } else {
               state.users = action.payload as [];
               state.status = 'success';
            }
         });
   },
});

export const selectStatus = (state: RootState) => state.admin.status;
export const selectError = (state: RootState) => state.admin.error;
export const selectCourses = (state: RootState) => state.admin.courses;
export const selectUsers = (state: RootState) => state.admin.users;

export default adminSlice.reducer;
