import React, { useState } from 'react';
import './Registration.scss';
import API from '../../common/apiUtils';

const Registration = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');

  const [emailText, setEmailText] = useState('');
  const onChangeEmail = e => setEmailText(e.target.value);

  const [firstNameText, setFirstNameText] = useState('');
  const onChangeFirstName = e => setFirstNameText(e.target.value);

  const [lastNameText, setLastNameText] = useState('');
  const onChangeLastName = e => setLastNameText(e.target.value);

  const [passwordText, setPasswordText] = useState('');
  const onChangePassword = e => setPasswordText(e.target.value);

  return (
    <div>
      <form className="registration-form" onSubmit={e => {
            e.preventDefault();
            setIsLoading(true);

            API.request('post', `api/v1/users`, {
              email: emailText,
              firstName: firstNameText,
              lastName: lastNameText,
              password: passwordText
            })
              .then((response) => {
                setEmailText('');
                setFirstNameText('');
                setLastNameText('');
                setPasswordText('');
                setError('SUCCESS!');
                setIsLoading(false);
              })
              .catch((error) => {
                setError(error.message);
                setIsLoading(false);
              });
            
          }}>
        <div>
          <label for="email">Email</label>
          <input type="text" placeholder="Enter Email" name="email" value={emailText} onChange={onChangeEmail} required />
        </div>
        <div>
          <label for="email">First name</label>
          <input type="text" placeholder="Enter first name" name="firstname" value={firstNameText} onChange={onChangeFirstName} required />
        </div>
        <div>
          <label for="email">Last name</label>
          <input type="text" placeholder="Enter last name" name="lastname" value={lastNameText} onChange={onChangeLastName} required />
        </div>
        <div>
          <label for="psw">Password</label>
          <input type="password" placeholder="Enter Password" name="psw" value={passwordText} onChange={onChangePassword} required />
        </div>
        <button type="submit" disabled={isLoading}>{ isLoading ? <span>Loading</span> : <span>Sign up</span> }</button>

        { error && 
            <div>Error: {error}</div>
          }
      </form>
    </div>
  );
}

export default Registration;
