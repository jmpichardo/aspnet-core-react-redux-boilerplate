import React from 'react';
import { connect } from 'react-redux';
import ApiUtils from '../../common/apiUtils';

class UserDetails extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      user: {},
      error: null
    }
  }

  componentDidMount() {
    ApiUtils.request('get', `api/v1/users/${this.props.auth.userId}`)
      .then((response) => {
        this.setState({ user: response });
      })
      .catch((error) => {
        this.setState({ error: error.message });
      });
  }

  render() {
    const { user, error } = this.state;

    return (
      <div>
        { error ?
          <div>{error}</div>
        :  
          <div>
            <div>{user.email}</div>
            <div>{user.firstName}</div>
            <div>{user.lastName}</div>
          </div>
        }
      </div>
    )
  }
}

const mapStateToProps = state => ({
  auth: state.auth
})

export default connect(mapStateToProps, null)(UserDetails);