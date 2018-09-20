import React from 'react';
import { Switch, Route } from 'react-router-dom';
import FikaList from './FikaList';
import FikaItem from './FikaItem';

const FikaRouter = () => (
    <Switch>
        <Route exact path="/fika" component={FikaList} />
        <Route path="/fika/:number" component={FikaItem} />
    </Switch>
);

export default FikaRouter;
