import React from 'react';
import Menu from './Menu';
import Body from './Body';
import ScrollToTop from './ScrollToTop';

function App() {
    return (
        <div className="full-height">
            <ScrollToTop>
                <Menu />
                <Body />
            </ScrollToTop>
        </div>
    );
}

export default App;
