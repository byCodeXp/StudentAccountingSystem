import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import userApi from "../api/userApi";
import { RootState } from "../app/store";

const initialState: IUserState = {
   users: [],
   status: 'idle',
   errorMessage: ''
};

export const getUsersAsync = createAsyncThunk('user/fetchUsers', async (request: { page: number, perPage: number }) => {
   return await userApi.fetchAll(request);
});

const userSlice = createSlice({
   name: 'user',
   initialState,
   reducers: {},
   extraReducers: (builder) => {
      builder
         .addCase(getUsersAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getUsersAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.users = action.payload.users;
         })
         .addCase(getUsersAsync.rejected, (state, action) => {
            state.status = 'failed';
            state.errorMessage = action.error.message ?? 'Something went wrong, try again please!';
         });
   }
});

export default userSlice.reducer;
export const selectUsers = (state: RootState) => state.user.users;
export const selectUserStatus = (state: RootState) => state.user.status;