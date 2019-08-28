import React from 'react';
import { connect } from 'react-redux';

const UserGreeting = (props) => {
  const { userName } = props;
  return <p>Hello {userName}</p>
}

const AnonymousGreeting = () => {
  return <p>Please login to continue...</p>
}

const Home = ({ auth }) => {
  const { userName } = auth;

  return (
    <div>
      { userName ? 
        <UserGreeting userName={userName} />
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
