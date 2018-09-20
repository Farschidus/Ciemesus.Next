import React from 'react';
import { Switch, Route } from 'react-router-dom';
import FikaList from '../Fika/FikaList';
import FikaRouter from '../Fika/FikaRouter';
import Profile from '../Profile';
import ScheduleList from '../Scheduler/ScheduleList';

const Body = () => (
    <main>
        <Switch>
            <Route exact path="/" component={FikaList} />
            <Route path="/fika" component={FikaRouter} />
            <Route path="/scheduler" component={ScheduleList} />
            <Route path="/team" component={ScheduleList} />
            <Route path="/profile" component={Profile} />
        </Switch>
    </main>
);

export default Body;
