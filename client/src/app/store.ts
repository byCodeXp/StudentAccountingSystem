import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import identityReducer from '../features/identitySlice';
import catalogReducer from '../features/catalogSlice';
import adminReducer from '../features/adminSlice';

export const store = configureStore({
  reducer: {
    identity: identityReducer,
    catalog: catalogReducer,
    admin: adminReducer
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
