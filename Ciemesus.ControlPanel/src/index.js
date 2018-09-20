import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './components/App';

import './assets/css/normalize.css';
import './assets/css/style.css';

render(
    (
        <BrowserRouter>
            <App />
        </BrowserRouter>
    ),
    document.getElementById('app'),
);
