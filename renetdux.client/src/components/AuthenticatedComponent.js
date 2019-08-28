import React from 'react';
import { connect } from 'react-redux';

export function requireAuthentication(Component) {

  class AuthenticatedComponent extends React.Component {
    componentWillMount() {
      this.checkAuth();
    }

    componentWillReceiveProps(nextProps) {
      this.checkAuth();
    }

    checkAuth() {
      if (!this.props.isAuthenticated) {
        this.props.history.push('/login');
      }
    }

    render() {
      return (
        <div>
          {this.props.isAuthenticated === true
            ? <Component {...this.props}/>
            : null
          }
        </div>
      )
    }
  }

  const mapStateToProps = (state) => ({
    isAuthenticated: state.auth.isAuthenticated
  });

  return connect(mapStateToProps)(AuthenticatedComponent);
}