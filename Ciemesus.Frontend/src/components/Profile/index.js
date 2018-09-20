import React from 'react';
import { Link } from 'react-router-dom';

const Profile = () => (
    <section>
        <h1>User Profile Placeholder page</h1>
        <Link className="Button Button--primary" to="/login">Log out</Link>
    </section>
);

export default Profile;
