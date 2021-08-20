import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import userReducer from '../features/user/userSlice';
import courseReducer from '../features/course/courseSlice';
import adminReducer from '../features/admin/adminSlice';

export const store = configureStore({
   reducer: {
      user: userReducer,
      course: courseReducer,
      admin: adminReducer,
   },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
