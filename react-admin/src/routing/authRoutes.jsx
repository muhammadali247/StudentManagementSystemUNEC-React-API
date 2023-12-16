import AuthLayout from "../layouts/Auth/AuthLayout";
import { Navigate } from 'react-router-dom';
import BASE_URL from "../constants/URLs";
import {
  SignUp,
  SignIn,
  VerifyAccount,
  ForgotPassword,
  ResetPasswordMessage,
  ResetPassword,
} from "../pages/Auth/index";
import { Error404, Error500 } from "../pages/Admin";
import { findPathByLabel } from "../utils/routingUtils";

const authRoutes = [
  {
    // path: `${BASE_URL}`, element: <SignUp />, label: "SignUp",
    path: "/auth",
    element: <AuthLayout />,
    errorElement: <Error404 />,
    children: [
      // { path: "teacherSignUp", element: <SignUp />, label: "teacherSignUp" },
      { path: "SignUp", element: <SignUp />, label: "SignUp" },
      { path: "signIn", element: <SignIn />, label: "SignIn" },
      {
        path: "verifyAccount/:userId",
        element: <VerifyAccount />,
        label: "VerifyAccount",
      },
      {
        path: "forgotPassword",
        element: <ForgotPassword />,
        label: "ForgotPassword",
      },
      {
        path: "resetPasswordMessage",
        element: <ResetPasswordMessage />,
        label: "ResetPasswordMessage",
      },
      {
        path: "resetPassword",
        element: <ResetPassword />,
        label: "ResetPassword",
      },
    ],
  },
  { path: "error500", element: <Error500 /> },
];

export default authRoutes;
