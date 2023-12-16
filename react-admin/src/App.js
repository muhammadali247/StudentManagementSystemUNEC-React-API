import adminRoutes from "./routing/adminRoutes";
// import clientRoutes from "./routing/clientRoutes";
import authRoutes from "./routing/authRoutes";
// import "./App.css";

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  Link,
} from "react-router-dom";
import React, { useEffect } from "react";
import { SnackbarProvider } from "notistack";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";
import axios from "axios";
// import AuthProvider from '@/components/Auth/AuthProvider/AuthProvider';
import AuthProvider from './components/Auth/AuthProvider/AuthProvider';

function App() {
  // const allRoutes = [...adminRoutes, ...clientRoutes, ...authRoutes];
  const allRoutes = [...adminRoutes, ...authRoutes];
  const router = createBrowserRouter(allRoutes);

  useEffect(() => {
    axios.defaults.withCredentials = true;
  }, []);

  useEffect(() => {
    router.navigate("/auth/signIn");
  }, [router]);

  return (
    <>
      <LocalizationProvider dateAdapter={AdapterMoment}>
        <SnackbarProvider
          maxSnack={3}
          anchorOrigin={{ vertical: "top", horizontal: "right" }}
        >
          <AuthProvider>
            <RouterProvider router={router}>
              
            </RouterProvider>
          </AuthProvider>
        </SnackbarProvider>
      </LocalizationProvider>
    </>
  );
}

export default App;