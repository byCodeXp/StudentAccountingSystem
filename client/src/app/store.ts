import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import identityReducer from '../features/identitySlice';
import courseReducer from '../features/courseSlice';
import categoryReducer from '../features/categorySlice';
import useReducer from '../features/userSlice';

export const store = configureStore({
  reducer: {
    identity: identityReducer,
    course: courseReducer,
    category: categoryReducer,
    user: useReducer
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
