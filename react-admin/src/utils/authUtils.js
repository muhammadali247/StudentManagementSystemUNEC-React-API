import axios from 'axios';
import jwtDecode from 'jwt-decode';

export const setAuthToken = (token) => {
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    const decodedToken = jwtDecode(token);
    const expirationTime = decodedToken.exp * 1000;
    localStorage.setItem('authToken', token);
    localStorage.setItem('authTokenExpiration', expirationTime);
  } else {
    delete axios.defaults.headers.common['Authorization'];
    localStorage.removeItem('authToken');
    localStorage.removeItem('authTokenExpiration');
  }
};