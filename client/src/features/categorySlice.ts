import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import categoryApi from '../api/categoryApi';
import { RootState } from '../app/store';

const initialState: ICategoryState = {
   categories: [],
   status: 'idle',
   errorMessage: '',
};

export const getCategoriesAsync = createAsyncThunk(
   'category/fetchCategories',
   async () => {
      return await categoryApi.fetchAll();
   }
);

export const createCategoryAsync = createAsyncThunk('category/fetchCreate', async (category: ICategory) => {
   await categoryApi.fetchCreate(category);
   return category;
});

export const updateCategoryAsync = createAsyncThunk('category/fetchUpdate', async (category: ICategory) => {
   await categoryApi.fetchUpdate(category);
   return category;
});

export const deleteCategoryAsync = createAsyncThunk('category/fetchDelete', async (id: string) => {
   categoryApi.fetchDelete(id);
   return id;
});

const categorySlice = createSlice({
   name: 'category',
   initialState,
   reducers: {},
   extraReducers: (builder) => {
      builder
         // Get categories
         .addCase(getCategoriesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getCategoriesAsync.fulfilled, (state, action) => {
            state.categories = action.payload;
            state.status = 'success';
         })
         .addCase(getCategoriesAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage =
               action.error.message ??
               'Something went wrong, please try again !';
         })
         // Create category
         .addCase(createCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(createCategoryAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.categories = [...state.categories, action.payload];
         })
         .addCase(createCategoryAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please!'
         })
         // Update category
         .addCase(updateCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(updateCategoryAsync.fulfilled, (state, action) => {
            state.status = 'success';
            const index = state.categories.findIndex(m => m.id === action.payload.id);
            state.categories = [...state.categories.slice(0, index), action.payload, ...state.categories.slice(index + 1)];
         })
         .addCase(updateCategoryAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please!';
         })
         // Delete category
         .addCase(deleteCategoryAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(deleteCategoryAsync.fulfilled, (state, action) => {
            state.status = 'success';
            const index = state.categories.findIndex(m => m.id === action.payload);
            if (index !== -1) {
               state.categories = [...state.categories.slice(0, index), ...state.categories.slice(index + 1)];
            }
         })
         .addCase(deleteCategoryAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please!';
         });
   },
});

export default categorySlice.reducer;
export const selectCategories = (state: RootState) => state.category.categories;
export const selectStatus = (state: RootState) => state.category.status;
export const selectError = (state: RootState) => state.category.errorMessage;
