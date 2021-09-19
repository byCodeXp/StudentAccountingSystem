import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import courseApi from '../api/courseApi';
import { RootState } from "../app/store";

const initialState: ICourseState = {
   currentCourse: undefined,
   courses: [],
   totalCount: 0,
   status: 'idle',
   errorMessage: '',
}

export const getCoursesAsync = createAsyncThunk('course/fetchCourses', async (request: ICoursesRequest) => {
   return await courseApi.fetchAll(request);
});

export const getOneCourseAsync = createAsyncThunk('course/fetchOneCourse', async (id: string) => {
   return await courseApi.fetchOne(id);
});

export const addCourseAsync = createAsyncThunk('course/fetchAdd', async (course: ICourse) => {
   return await courseApi.fetchAdd(course);
});

export const updateCourseAsync = createAsyncThunk('course/fetchUpdate', async (course: ICourse) => {
   return await courseApi.fetchUpdate(course);
});

export const deleteCourseAsync = createAsyncThunk('course/fetchDelete', async (id: string) => {
   await courseApi.fetchDelete(id);
   return id;
});

const courseSlice = createSlice({
   name: 'course',
   initialState,
   reducers: {
      resetStatusCourse: (state) => {
         state.status = 'idle';
      }
   },
   extraReducers: (builder) => {
      builder
         // Get all courses
         .addCase(getCoursesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getCoursesAsync.fulfilled, (state, action) => {
            state.courses = action.payload.courses;
            state.totalCount = action.payload.totalCount;
            state.status = 'success';
         })
         .addCase(getCoursesAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, please try again !';
         })
         // Get one course
         .addCase(getOneCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getOneCourseAsync.fulfilled, (state, action) => {
            state.currentCourse = action.payload;
            state.status = 'success';
         })
         .addCase(getOneCourseAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, please try again !';
         })
         // Add course
         .addCase(addCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(addCourseAsync.fulfilled, (state, action) => {
            state.status = 'success';
         })
         .addCase(addCourseAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, please try again !';
         })
         // Update course
         .addCase(updateCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(updateCourseAsync.fulfilled, (state, action) => {
            state.status = 'success';
            const course = action.payload as ICourse;
            const index = state.courses.findIndex(m => m.id === course.id);
            state.courses = [...state.courses.slice(0, index), course, ...state.courses.slice(index + 1)]
         })
         .addCase(updateCourseAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, please try again !';
         })
         // Delete
         .addCase(deleteCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(deleteCourseAsync.fulfilled, (state, action) => {
            state.status = 'deleted';
            const index = state.courses.findIndex(m => m.id === action.payload);
            if (index !== -1) {
               state.courses = [...state.courses.slice(0, index), ...state.courses.slice(index + 1)];
            }
         })
         .addCase(deleteCourseAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, please try again !';
         });
   }
});

export default courseSlice.reducer;
export const { resetStatusCourse } = courseSlice.actions;
export const selectCurrentCourse = (state: RootState) => state.course.currentCourse;
export const selectCourses = (state: RootState) => state.course.courses;
export const selectCount = (state: RootState) => state.course.totalCount;
export const selectStatus = (state: RootState) => state.course.status;