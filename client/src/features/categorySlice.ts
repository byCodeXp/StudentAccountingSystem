import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../app/store';
import categoryApi from '../api/categoryApi';

const initialState: ICategoryState = {
   categories: [],
   status: 'idle',
   error: '',
};

export const getCategoriesAsync = createAsyncThunk('category/fetchCategories', async () => {
   return await categoryApi.fetchCategories();
});

export const categorySlice = createSlice({
   name: 'category',
   initialState,
   reducers: {},
   extraReducers(builder) {
      builder
         .addCase(getCategoriesAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getCategoriesAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.categories = action.payload;
         });
   },
});

export const selectStatus = (state: RootState) => state.category.status;
export const selectCategories = (state: RootState) => state.category.categories;
export default categorySlice.reducer;
