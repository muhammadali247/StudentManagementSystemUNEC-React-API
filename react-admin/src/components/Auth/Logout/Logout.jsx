import React from 'react';
import axiosInstance from '../../../utils/axiosInstance';
import { useNavigate } from 'react-router-dom';
import { ACCOUNT_LOGOUT_URL } from '../../../constants/URLs';
import { findPathByLabel } from '../../../utils/routingUtils';
import authRoutes from '../../../routing/authRoutes';
import { useSnackbar } from 'notistack';
import useService from '../../../hooks';

function Logout({ children }) {
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const { authServices } = useService();

  const handleLogout = async () => {
    try {
      // const response = await authServices.postLogout();
      // const response = await axiosInstance.post(ACCOUNT_LOGOUT_URL);
      const response = await authServices.Logout();
      console.log(response.data);
      // Clear authentication-related data from local storage
      localStorage.removeItem('accessToken');
      localStorage.removeItem('tokenExpiration');

      enqueueSnackbar("Logout done successfully!", {
        variant: "success",
        autoHideDuration: 1500,
      });

      // Redirect to the login page
      navigate(findPathByLabel(authRoutes, 'SignIn'));
    } catch (error) {
      // Handle errors, such as network issues or server errors
      console.error('Logout failed:', error);

      let errorMessage = 'An error occurred while logging out.';
      if (error.response) {
        if (error.response.status === 500) {
          // Handle server error and navigate to an error page
          navigate('/error500');
          return;
        }
        // Handle other server errors or display a generic error message
        errorMessage =
          'Unable to log out at the moment. Please try again later.';
      }

      // Show an error message to the user
      enqueueSnackbar(errorMessage, {
        variant: 'error',
      });
    }
  };

  return (
    <button onClick={handleLogout} style={{ backgroundColor: 'transparent', border: 'none', cursor: 'pointer' }}>
      {children || 'Logout'}
    </button>
  );
}

export default Logout;
