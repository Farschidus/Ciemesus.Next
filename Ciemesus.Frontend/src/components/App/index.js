import React from 'react';
import PropTypes from 'prop-types';
import Menu from './Menu';
import Body from './Body';
import Header from './Header';
import ScrollToTop from './ScrollToTop';
import AuthBody from './../Authentication/AuthBody';

function App(props) {
    const content = props.store.isAuthenticated === true
        ? <ScrollToTop><Header /><Body /><Menu /></ScrollToTop>
        : <AuthBody />;

    return (
        <div className="full-height">
            {content}
        </div>
    );
}

App.propTypes = {
    store: PropTypes.object.isRequired,
};

export default App;
