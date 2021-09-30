import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import categoryApi from "../api/categoryApi";
import courseApi from "../api/courseApi";
import userApi from "../api/userApi";
import { RootState } from "../app/store";

interface AdminState {
   categories: ICategory[];
   users: IUser[];
   totalUsers: number;
   courses: ICourse[];
   totalCourses: number;
   status: 'idle' | 'loading' | 'success' | 'failed';
   error: string;
}

const initialState: AdminState = {
   categories: [],
   users: [],
   totalUsers: 0,
   courses: [],
   totalCourses: 0,
   status: 'idle',
   error: '',
};

// Categories actions
export const loadCategoriesAsync = createAsyncThunk('ADMIN/LOAD_CATEGORIES', async (request: { search: string }) => {
   return await categoryApi.fetchAll(request);
});

export const createCategoryAsync = createAsyncThunk('ADMIN/CREATE_CATEGORY', async (category: ICategory) => {
   return await categoryApi.fetchCreate(category);
});

export const updateCategoryAsync = createAsyncThunk('ADMIN/UPDATE_CATEGORY', async (category: ICategory) => {
   return await categoryApi.fetchUpdate(category);
});

export const deleteCategoryAsync = createAsyncThunk('ADMIN/DELETE_CATEGORY', async (id: string) => {
   await categoryApi.fetchDelete(id);
   return id;
})

// Courses actions
export const loadCoursesAsync = createAsyncThunk('ADMIN/LOAD_COURSES', async (request: ICoursesRequest) => {
   return await courseApi.fetchAll(request);
});

export const createCourseAsync = createAsyncThunk('ADMIN/CREATE_COURSE', async (course: ICourse) => {
   return await courseApi.fetchAdd(course);
});

export const updateCourseAsync = createAsyncThunk('ADMIN/UPDATE_COURSE', async (course: ICourse) => {
   return await courseApi.fetchUpdate(course);
});

export const deleteCourseAsync = createAsyncThunk('ADMIN/DELETE_COURSE', async (id: string) => {
   await courseApi.fetchDelete(id);
   return id;
});

// User actions
export const loadUsersAsync = createAsyncThunk('ADMIN/LOAD_USERS', async (request: { search: string, page: number, perPage: number }) => {
   return await userApi.fetchAll(request);
});

const adminSlice = createSlice({
   name: 'ADMIN',
   initialState,
   reducers: {},
   extraReducers: (builder) => {
      builder
         // Load users
         .addCase(loadUsersAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loadUsersAsync.fulfilled, (state, { payload }) => {
            state.users = payload.users;
            state.totalUsers = payload.totalCount;
            state.status = 'success';
         })
         .addCase(loadUsersAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Load categories
         .addCase(loadCategoriesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loadCategoriesAsync.fulfilled, (state, { payload }) => {
            state.categories = payload;
            state.status = 'success';
         })
         .addCase(loadCategoriesAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Create category
         .addCase(createCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(createCategoryAsync.fulfilled, (state, { payload }) => {
            state.categories = [...state.categories, payload];
            state.status = 'success';
         })
         .addCase(createCategoryAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Update category
         .addCase(updateCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(updateCategoryAsync.fulfilled, (state, { payload }) => {
            const index = state.courses.findIndex(m => m.id === payload.id);
            if (index !== -1) {
               state.categories = [...state.categories.slice(0, index), payload, ...state.categories.slice(index + 1)];
            }
            state.status = 'success';
         })
         .addCase(updateCategoryAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Delete category
         .addCase(deleteCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(deleteCategoryAsync.fulfilled, (state, { payload }) => {
            const index = state.categories.findIndex(m => m.id === payload);
            if (index !== -1) {
               state.categories = [...state.categories.slice(0, index), ...state.categories.slice(index + 1)];
            }
            state.status = 'success';
         })
         .addCase(deleteCategoryAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Load courses
         .addCase(loadCoursesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loadCoursesAsync.fulfilled, (state, { payload }) => {
            state.courses = payload.courses;
            state.totalCourses = payload.totalCount;
            state.status = 'success';
         })
         .addCase(loadCoursesAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Create course
         .addCase(createCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(createCourseAsync.fulfilled, (state, { payload }) => {
            state.courses = [...state.courses, payload];
            state.status = 'success';
         })
         .addCase(createCourseAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Update course
         .addCase(updateCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(updateCourseAsync.fulfilled, (state, { payload }) => {
            const index = state.courses.findIndex(m => m.id === payload.id);
            if (index !== -1) {
               state.courses = [...state.courses.slice(0, index), payload, ...state.courses.slice(index + 1)];
            }
            state.status = 'success';
         })
         .addCase(updateCourseAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Delete course
         .addCase(deleteCourseAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(deleteCourseAsync.fulfilled, (state, { payload }) => {
            const index = state.courses.findIndex(m => m.id === payload);
            if (index !== -1) {
               state.courses = [...state.courses.slice(0, index), ...state.courses.slice(index + 1)];
            }
            state.status = 'success';
         })
         .addCase(deleteCourseAsync.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         });
   }
});

export default adminSlice.reducer;
export const selectStatus = (state: RootState) => state.admin.status;
export const selectCourses = (state: RootState) => state.admin.courses;
export const selectCoursesCount = (state: RootState) => state.admin.totalCourses;
export const selectCategories = (state: RootState) => state.admin.categories;
export const selectUsers = (state: RootState) => state.admin.users;