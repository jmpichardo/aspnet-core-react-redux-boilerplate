import React from 'react';
import './Login.scss';

const Login = () => {
  return (
    <div>
      <form className="login-form">
        <div>
          <label for="email">Email</label>
          <input type="text" placeholder="Enter Email" name="email" required />
        </div>
        <div>
          <label for="psw">Password</label>
          <input type="password" placeholder="Enter Password" name="psw" required />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  );
}

export default Login;
