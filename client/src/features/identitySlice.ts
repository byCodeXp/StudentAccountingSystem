import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { RootState } from '../app/store';
import identityApi from '../api/identityApi';
import { tokenUtil } from '../utils/tokenUtil';

const initialState: IdentityState = {
   user: null,
   status: 'guest',
   error: '',
};

export const loginAsync = createAsyncThunk(
   'identity/fetchLogin',
   async (request: ILoginRequest) => {
      const response = await identityApi.fetchLogin(request);
      tokenUtil.set(response);
      return tokenUtil.user(response);
   }
);

export const registerAsync = createAsyncThunk(
   'identity/fetchRegister',
   async (request: IRegisterRequest) => {
      return await identityApi.fetchRegister(request);
   }
);

const identitySlice = createSlice({
   name: 'identity',
   initialState,
   reducers: {
      logout: (state) => {
         state.status = 'guest';
         state.user = null;
         tokenUtil.clear();
      },
      resetStatus: (state) => {
         state.status = 'guest';
      },
      loadUser: (state) => {
         const user = tokenUtil.user();
         if (user) {
            if (user.exp < new Date().getTime() / 1000) {
               tokenUtil.clear();
            } else {
               state.user = user;
               state.status = 'signed';
            }
         }
      },
   },
   extraReducers(builder) {
      builder
         .addCase(loginAsync.pending, (state) => {
            state.status = 'loading';
         })
         .addCase(loginAsync.fulfilled, (state, action) => {
            state.status = 'signed';
            state.user = {
               id: action.payload.id,
               email: action.payload.email,
               firstName: action.payload.firstName,
               lastName: action.payload.lastName,
               age: 0,
               courses: action.payload.courses,
               role: action.payload.role,
            };
         })
         .addCase(loginAsync.rejected, (state, action) => {
            if (state.status === 'loading') {
               state.status = 'failed';
               state.error = action.payload as string;
            }
         });
   },
});

export default identitySlice.reducer;
export const { logout, resetStatus, loadUser } = identitySlice.actions;

export const selectUser = (state: RootState) => state.identity.user;
export const selectStatus = (state: RootState) => state.identity.status;
export const selectError = (state: RootState) => state.identity.error;
