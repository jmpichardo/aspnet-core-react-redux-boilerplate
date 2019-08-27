import React from 'react';
import './Registration.scss';

const Registration = () => {
  return (
    <div>
      <form className="registration-form">
        <div>
          <label for="email">Email</label>
          <input type="text" placeholder="Enter Email" name="email" required />
        </div>
        <div>
          <label for="email">First name</label>
          <input type="text" placeholder="Enter first name" name="firstname" required />
        </div>
        <div>
          <label for="email">Last name</label>
          <input type="text" placeholder="Enter last name" name="lastname" required />
        </div>
        <div>
          <label for="psw">Password</label>
          <input type="password" placeholder="Enter Password" name="psw" required />
        </div>
        <div>
          <label for="psw">Repeat password</label>
          <input type="password" placeholder="Repeat Password" name="psw2" required />
        </div>
        <button type="submit">Register</button>
      </form>
    </div>
  );
}

export default Registration;
