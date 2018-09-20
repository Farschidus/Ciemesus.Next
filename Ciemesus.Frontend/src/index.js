import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './components/App';
import { AuthenticationProvider, AuthenticationConsumer } from './components/Utils/Authentication';

import './assets/css/normalize.css';
import './assets/css/style.css';

function registerServiceWorkers() {
    if ('serviceWorker' in navigator) {
        window.addEventListener('load', () => {
            navigator.serviceWorker.register('./sw.js').then((registration) => {
                console.log('SW registered: ', registration);
            }).catch((error) => {
                console.log('SW registration failed: ', error);
            });
        });
    }
}

render(
    (
        <BrowserRouter>
            <AuthenticationProvider>
                <AuthenticationConsumer>
                    { (store) => <App store={store} /> }
                </AuthenticationConsumer>
            </AuthenticationProvider>
        </BrowserRouter>
    ), document.getElementById('app'),
);
registerServiceWorkers();

