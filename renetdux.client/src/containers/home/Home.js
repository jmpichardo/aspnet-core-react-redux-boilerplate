import React from 'react';
import { connect } from 'react-redux';

const UserGreeting = (props) => {
  const { username } = props;
  return <p>Hello {username}</p>
}

const AnonymousGreeting = () => {
  return <p>Please login to continue...</p>
}

const Home = ({ auth }) => {
  const { username } = auth;

  return (
    <div>
      { username ? 
        <UserGreeting username={username} />
      :
        <AnonymousGreeting />
      }
      
    </div>
  )
}


const mapStateToProps = state => ({
  auth: state.auth
})

export default connect(mapStateToProps, null)(Home);
