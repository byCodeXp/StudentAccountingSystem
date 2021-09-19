import React from 'react';
import { Navigate, Route } from 'react-router';

export const PrivateRoute = (props: { path: string, element: any, condition: boolean, redirect: string }) => {
   const { path, element, condition, redirect } = props;

   if (condition === false) {
      return <Navigate to={redirect} />
   }

   return (
      <Route path={path} element={element} />
   );
}