import React from 'react';
import PropTypes from 'prop-types';
import { withRouter } from 'react-router';

class ScrollToTop extends React.Component {
    componentDidUpdate(nextState) {
        if (nextState.history.action !== 'POP') {
            window.scrollTo(0, 0);
        }
    }

    render() {
        return this.props.children;
    }
}

ScrollToTop.propTypes = {
    children: PropTypes.node,
};

export default withRouter(ScrollToTop);
