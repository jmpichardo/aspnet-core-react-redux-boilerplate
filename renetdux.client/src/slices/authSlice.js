import { createSlice } from 'redux-starter-kit';
import API from '../common/apiUtils';
import jwtDecode from 'jwt-decode';

const authSlice = createSlice({
  slice: 'auth',
  initialState: {
    token: null,
    refresh_token: null,
    expiration: 0,
    userName: '',
    userId: 0,
    isAuthenticated: false,
    isLoading: false,
    error: ''
  },
  reducers: {
    loginBegin(state) {
      return { ...state, isLoading: true, error: '' } 
    },
    loginSuccess(state, action) {
      const decodedToken = jwtDecode(action.payload.access_token);

      return { 
        ...state,
        isLoading: false,
        token: action.payload.access_token,
        refresh_token: action.payload.refresh_token,
        expiration: decodedToken.exp,
        userName: decodedToken.given_name,
        userId: decodedToken.nameid,
        isAuthenticated: true } 
    },
    loginError(state, action) {
      return { ...state, isLoading: false, error: action.payload } 
    },
    logout(state) {
      return { ...state, token: null, refresh_token: null, expiration: 0, userName: '', isAuthenticated: false }
    }
  }
})

export function login(email, password) {
  return dispatch => {
    dispatch(loginBegin());
    API.request('post', 'token', {
      grant_type: 'password',
      username: email,
      password: password
    })
      .then((response) => {
        dispatch(loginSuccess(response));
      })
      .catch((error) => {
        dispatch(loginError(error.message));
      });
  }
}

export const { loginBegin, loginSuccess, loginError, logout } = authSlice.actions;

export default authSlice.reducer;
