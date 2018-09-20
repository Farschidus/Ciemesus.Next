import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Home from '../Home';

const Body = () => (
    <main className="Body">
        <Switch>
            <Route exact path="/" component={Home} />
        </Switch>
    </main>
);

export default Body;
