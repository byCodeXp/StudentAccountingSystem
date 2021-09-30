import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import categoryApi from "../api/categoryApi";
import courseApi from "../api/courseApi";
import { RootState } from "../app/store";

interface ICatalogState {
   courses: ICourse[];
   totalCount: number;
   status: 'idle' | 'loading' | 'success' | 'failed';
   error: string;
   categories: ICategory[];
   request: {
      search: string;
      sortBy: Sort;
      categories: [];
      tags: [];
      page: number;
   }
}

const initialState: ICatalogState = {
   courses: [],
   totalCount: 0,
   status: 'idle',
   error: '',
   categories: [],
   request: {
      search: '',
      sortBy: 'Relevance',
      categories: [],
      tags: [],
      page: 1
   }
}

export const fetchCourses = createAsyncThunk('CATALOG/FETCH_COURSES', async (request: ICoursesRequest) => {
   return await courseApi.fetchAll(request);
});

export const fetchCategories = createAsyncThunk('CATALOG/FETCH_CATEGORIES', async (request: { search: string }) => {
   return await categoryApi.fetchAll(request);
});

const catalogSlice = createSlice({
   name: 'CATALOG',
   initialState,
   reducers: {
      changeSort: (state, action) => {
         state.request.sortBy = action.payload;
      },
      setTags: (state, { payload }) => {
         state.request.tags = payload;
      },
      setPage: (state, { payload }) => {
         state.request.page = payload;
      }
   },
   extraReducers: (builder) => {
      builder
         // Fetch courses
         .addCase(fetchCourses.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(fetchCourses.fulfilled, (state, { payload }) => {
            state.courses = payload.courses;
            state.totalCount = payload.totalCount;
            state.status = 'success';
         })
         .addCase(fetchCourses.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         })
         // Fetch categories
         .addCase(fetchCategories.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(fetchCategories.fulfilled, (state, { payload }) => {
            state.categories = payload;
            state.status = 'success';
         })
         .addCase(fetchCategories.rejected, (state, { error }) => {
            state.error = error.message ?? 'Something went wrong, please try again!';
            state.status = 'failed';
         });
   }
});

export default catalogSlice.reducer;
export const { changeSort, setTags, setPage } = catalogSlice.actions;
export const selectCourses = (state: RootState) => state.catalog.courses;
export const selectSort = (state: RootState) => state.catalog.request.sortBy;
export const selectCategories = (state: RootState) => state.catalog.categories;
export const selectTags = (state: RootState) => state.catalog.request.tags;
export const selectPage = (state: RootState) => state.catalog.request.page;
export const selectTotalCount = (state: RootState) => state.catalog.totalCount;