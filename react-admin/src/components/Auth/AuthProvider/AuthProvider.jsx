import React, { createContext, useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { jwtDecode } from 'jwt-decode';

export const tokenEmailProperty =
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
export const tokenUserNameProperty =
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
export const tokenRoleProperty =
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
export const tokenIdProperty =
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";


export const ValidateToken = ({ token }) => {
  const navigate = useNavigate();
  console.log("asd");
  if (token) {
    const decodedToken = jwtDecode(token);
    if (decodedToken[tokenRoleProperty] !== "Admin") {
      navigate("ErrorPage");
    }
  } else {
    navigate("ErrorPage");
  }
};

export const getToken = () => {
  const token = localStorage.getItem("token");
  if (token) {
    return token;
  }
};


export const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  const updateUser = (decodedToken) => {
    console.log('Decoded Token:', decodedToken);

    setUser({
      id: decodedToken[tokenIdProperty],
      email: decodedToken[tokenEmailProperty],
      username: decodedToken[tokenUserNameProperty],
      roles: decodedToken[tokenRoleProperty]
    });
  };

  useEffect(() => {
    const updateAuth = () => {
      const token = localStorage.getItem('accessToken');
      if (token) {
        console.log('Token from localStorage:', token);
        try {
          const decodedToken = jwtDecode(token);
          updateUser(decodedToken);
          console.log('Decoded Token:', decodedToken);
        } catch (error) {
          console.error('Error decoding token:', error);
          setUser(null); // Set user to null in case of token decoding errors.
        }
      } else {
        console.log('No token found in localStorage');
        setUser(null);
      }
    };

    updateAuth();

    window.addEventListener('storage', updateAuth);

    return () => {
      window.removeEventListener('storage', updateAuth);
    };
  }, []);

  const values = {
    user,
    updateUser,
  };

  return (
    <AuthContext.Provider value={values}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
