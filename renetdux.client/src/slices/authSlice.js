import { createSlice } from "redux-starter-kit";
import { apiPost } from "../common/apiUtils";

const authSlice = createSlice({
  slice: 'auth',
  initialState: {
    username: '',
    isLoading: false,
    error: ''
  },
  reducers: {
    loginBegin(state) {
      return { ...state, isLoading: true, error: '' } 
    },
    loginSuccess(state, action) {
      return { ...state, isLoading: false, username: action.payload } 
    },
    loginError(state, action) {
      return { ...state, isLoading: false, error: action.payload } 
    }
  }
})

export function login(email, password) {
  return dispatch => {
    dispatch(loginBegin());
    apiPost('token', {
      grant_type: 'password',
      username: email,
      password: password,
      refresh_token: ""
    })
      .then((response) => {
        dispatch(loginSuccess(response));
      })
      .catch((error) => {
        dispatch(loginError(error.message));
      });
  }
}

export const { loginBegin, loginSuccess, loginError } = authSlice.actions;

export default authSlice.reducer;
