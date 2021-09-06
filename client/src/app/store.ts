import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import identityReducer from '../features/identitySlice';
import categoryReducer from '../features/categorySlice';
import courseReducer from '../features/courseSlice';
import userReducer from '../features/userSlice';

export const store = configureStore({
   reducer: {
      identity: identityReducer,
      category: categoryReducer,
      course: courseReducer,
      user: userReducer,
   },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
   ReturnType,
   RootState,
   unknown,
   Action<string>
>;
