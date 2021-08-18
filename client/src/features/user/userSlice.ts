import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { fetchLogin, fetchRegister } from './userAPI';
import { statusCodeText } from '../httpStatusCodes';

const initialState: IUserState = {
   user: {
      firstName: '',
      lastName: '',
      email: '',
   },
   status: 'idle',
   error: '',
};

export const loginAsync = createAsyncThunk('user/fetchLogin', async (request: ILoginRequest): Promise<{} | number> => {
   return await fetchLogin(request);
});

export const registerAsync = createAsyncThunk('user/fetchRegister', async (request: IRegisterRequest): Promise<{} | number> => {
   return await fetchRegister(request);
});

export const userSlice = createSlice({
   name: 'user',
   initialState,
   reducers: {
      resetStatus: (state) => {
         state.status = 'idle';
      },
      logout: (state) => {
         localStorage.removeItem('access_token');
         state.user = initialState.user;
         state.status = 'idle';
      },
   },
   extraReducers: (builder) => {
      builder
         .addCase(loginAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loginAsync.fulfilled, (state, action) => {
            if (typeof action.payload === 'number') {
               state.error = statusCodeText(action.payload);
               state.status = 'error';
            } else {
               state.user = action.payload as IUser;
               state.status = 'signed';
            }
         })
         .addCase(registerAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(registerAsync.fulfilled, (state, action) => {
            if (action.payload === 400) {
               state.status = 'success';
            } else {
               state.status = 'error';
            }
         });
   },
});

export const selectStatus = (state: RootState) => state.user.status;
export const selectError = (state: RootState) => state.user.error;
export const selectUser = (state: RootState) => state.user.user;

export const { resetStatus, logout } = userSlice.actions;

export default userSlice.reducer;
