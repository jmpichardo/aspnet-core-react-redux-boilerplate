import React from 'react';
import ApiUtils from '../../common/apiUtils';

class Users extends React.Component {

  componentDidMount() {
    ApiUtils.request('get', 'api/v1/users/1')
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  render() {
    return (
      <div>
        <p>List of users</p>
      </div>
    )
  }
}

export default Users;
