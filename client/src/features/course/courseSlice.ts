import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { fetchCourses, fetchCourse } from './courseAPI';
import { statusCodeText } from '../httpStatusCodes';

const initialState: ICourseState = {
   courses: [],
   currentCourse: {
      id: '',
      title: '',
      description: '',
      preview: '',
   },
   status: 'idle',
   error: '',
};

export const getCoursesAsync = createAsyncThunk('course/fetchCourses', async (page: number): Promise<{} | number> => {
   return await fetchCourses(page);
});

export const getCourseAsync = createAsyncThunk('course/fetchCourse', async (id: string): Promise<{} | number> => {
   return await fetchCourse(id);
});

export const courseSlice = createSlice({
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
         .addCase(getCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getCourseAsync.fulfilled, (state, action) => {
            if (typeof action.payload === 'number') {
               state.status = 'error';
               state.error = statusCodeText(action.payload);
            } else {
               state.currentCourse = action.payload as ICourse;
               state.status = 'success';
            }
         });
   },
});

export const selectStatus = (state: RootState) => state.course.status;
export const selectError = (state: RootState) => state.course.error;
export const selectCourses = (state: RootState) => state.course.courses;
export const selectCourse = (state: RootState) => state.course.currentCourse;

export default courseSlice.reducer;
