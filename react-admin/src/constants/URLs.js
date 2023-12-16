// const BaseURL = "https://localhost:7009/api";
// const BaseURL = "https://localhost:7251/api";

export const BASE_WEB_URL = "https://localhost:7009";
const BASE_URL = `${BASE_WEB_URL}/api`; //api

export default BASE_URL;






























// export const ACCOUNT_RENEW_TOKEN_URL = `${BASE_URL}/Accounts/renew-token`;
export const ACCOUNT_RENEW_TOKEN_URL = `${BASE_URL}/Authentication/renew-token`;
export const USERS_GET_BY_ID_LIMITED_URL = (userId) =>  `${BASE_URL}/Accounts/${userId}`;
export const ACCOUNT_LOGOUT_URL = `${BASE_URL}/Authentication/logout`;

// export const ARTIST_CREATE_URL = `${BASE_URL}/Artists`;
// export const MUSICAL_PROJECTS_GETALL_URL = `${BASE_URL}/MusicalProjects`;
// export const ARTIST_GETALL_URL = `${BASE_URL}/Artists`;
// export const ARTIST_DELETE_URL = (artistId) => `${BASE_URL}/Artists/${artistId}`;
export const ACCOUNT_REGISTER_URL = `${BASE_URL}/Accounts/Register`