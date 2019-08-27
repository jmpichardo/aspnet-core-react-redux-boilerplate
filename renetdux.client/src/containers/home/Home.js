import React from 'react';

const UserGreeting = (props) => {
  const { username } = props;
  return <p>Hello {username}</p>
}

const AnonymousGreeting = () => {
  return <p>Please login to continue...</p>
}

const Home = (props) => {
  const { username } = props;

  return (
    <div>
      { username ? 
        <UserGreeting />
      :
        <AnonymousGreeting />
      }
      
    </div>
  )
}

export default Home;
