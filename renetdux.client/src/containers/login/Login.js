import React, { useState } from 'react';
import { connect } from 'react-redux';
import { login } from '../../slices/authSlice';
import './Login.scss';

const Login = ({ login, auth }) => {
  const { isLoading, error } = auth;

  const [nameText, setNameText] = useState('');
  const onChangeName = e => setNameText(e.target.value);

  const [passwordText, setPasswordText] = useState('');
  const onChangePassword = e => setPasswordText(e.target.value);

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
          <div>Error: {error}</div>
        }
      </form>
    </div>
  );
}

const mapStateToProps = state => ({
  auth: state.auth
});

const mapDispatch = { login }

export default connect(mapStateToProps, mapDispatch)(Login);