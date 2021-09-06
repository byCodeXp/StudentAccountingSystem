import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import courseApi from '../api/courseApi';
import { RootState } from '../app/store';

const initialState: ICourseState = {
   courses: [],
   count: 0,
   currentCourse: null,
   status: 'idle',
   error: '',
};

export const getCoursesAsync = createAsyncThunk(
   'course/fetchCourses',
   async (request: ICoursesRequest) => {
      return await courseApi.fetchCourses(request);
   }
);

export const getOneCourseAsync = createAsyncThunk('course/fetchOneCourse', async (id: string) => {
   return await courseApi.fetchOneCourse(id);
});

export const addCourseAsync = createAsyncThunk('course/fetchCreate', async (course: ICourse) => {
   return await courseApi.fetchAddCourse(course);
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
            if (action) state.status = 'success';
            state.courses = action.payload.courses;
            state.count = action.payload.totalCount;
         })
         .addCase(getCoursesAsync.rejected, (state, action) => {
            if (state.status === 'loading') {
               state.status = 'error';
            }
         })
         .addCase(getOneCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getOneCourseAsync.fulfilled, (state, action) => {
            if (action) state.status = 'success';
            state.currentCourse = action.payload;
         })
         .addCase(getOneCourseAsync.rejected, (state) => {
            if (state.status === 'loading') {
               state.status = 'error';
            }
         })
         .addCase(addCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(addCourseAsync.fulfilled, (state) => {
            state.status = 'success';
         })
         .addCase(addCourseAsync.rejected, (state) => {
            if (state.status === 'loading') {
               state.status = 'error';
            }
         });
   },
});

export const selectStatus = (state: RootState) => state.course.status;
export const selectCourses = (state: RootState) => state.course.courses;
export const selectCount = (state: RootState) => state.course.count;
export const selectCurrentCourse = (state: RootState) => state.course.currentCourse;
export default courseSlice.reducer;
