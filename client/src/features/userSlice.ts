import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../app/store';
import userApi from '../api/userApi';

const initialState: IUserState = {
   users: [],
   status: 'idle',
   error: '',
};

export const getUsersAsync = createAsyncThunk('user/fetchUsers', async (request: IUserRequest) => {
   return await userApi.fetchUsers(request);
});

export const userSlice = createSlice({
   name: 'user',
   initialState,
   reducers: {},
   extraReducers(builder) {
      builder
         .addCase(getUsersAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(getUsersAsync.fulfilled, (state, action) => {
            state.status = 'success';
            state.users = action.payload.users;
         })
         .addCase(getUsersAsync.rejected, (state, action) => {
            if (state.status === 'loading') {
               state.status = 'error';
               state.error = action.payload as string;
            }
         });
   },
});

export default userSlice.reducer;
export const selectUsers = (state: RootState) => state.user.users;
