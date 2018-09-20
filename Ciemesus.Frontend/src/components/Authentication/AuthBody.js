import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Login from './Login';
import ForgottenPassword from './ForgottenPassword';
import Register from './Register';

const Body = () => (
    <main>
        <Switch>
            <Route path="/login" component={Login} />
            <Route path="/forgottenPassword" component={ForgottenPassword} />
            <Route path="/register" component={Register} />
        </Switch>
    </main>
);

export default Body;
