import React from 'react';
import ApiUtils from '../../common/apiUtils';

class Users extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      users: [],
      error: null
    }
  }

  componentDidMount() {
    ApiUtils.request('get', 'api/v1/users/')
      .then((response) => {
        this.setState({ users: response });
      })
      .catch((error) => {
        this.setState({ error: error.message });
      });
  }

  render() {
    const { users, error } = this.state;

    return (
      <div>
        <p>List of users</p>
        { error ? 
          <div>{error}</div>
        :
          <div>{users}</div>
        }
      </div>
    )
  }
}

export default Users;