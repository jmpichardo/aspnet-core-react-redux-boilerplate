import { createSlice } from "redux-starter-kit";

const authSlice = createSlice({
  slice: 'auth',
  initialState: {
    username: '',
    password: null,
    loginOn: null
  },
  reducers: {
    login: {
      reducer(state, action) { 
        // This is the action
        return { 
          ...state, 
          username: action.payload.name, 
          password: action.payload.password,
          loginOn: action.payload.loginOn } 
      },
      prepare(name, password) {
        return { payload: { name, password, loginOn: new Date() } }
      }
    },
    logout(state, action) {
      return { ...state, username: '', password: null, loginOn: null } 
    }
  }
})

export const { login, logout } = authSlice.actions;

export default authSlice.reducer;
