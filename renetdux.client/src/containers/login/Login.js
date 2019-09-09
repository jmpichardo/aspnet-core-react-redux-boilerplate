import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { login } from '../../slices/authSlice';
import './Login.scss';

const Login = ({ login, auth }) => {
  const { isLoading, error, isAuthenticated } = auth;

  const [nameText, setNameText] = useState('');
  const onChangeName = e => setNameText(e.target.value);

  const [passwordText, setPasswordText] = useState('');
  const onChangePassword = e => setPasswordText(e.target.value);

  if(isAuthenticated) {
    return <Redirect to='/' />
  } else {
    return (
      <div>
        <form className="login-form" onSubmit={e => {
            e.preventDefault()
            if (!nameText.trim()) {
              return
            }
            login(nameText, passwordText);
          }}>
          <div>
            <label htmlFor="email">Email</label>
            <input type="text" name="email" placeholder="Enter Email" value={nameText} onChange={onChangeName} required />
          </div>
          <div>
            <label htmlFor="psw">Password</label>
            <input type="password" name="psw" placeholder="Enter Password" value={passwordText} onChange={onChangePassword} required />
          </div>
          <button type="submit" disabled={isLoading}>{ isLoading ? <span>Loading</span> : <span>Login</span> }</button>

          { error && 
            <div>{error}</div>
          }
        </form>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  auth: state.auth
});

const mapDispatch = { login }

export default connect(mapStateToProps, mapDispatch)(Login);