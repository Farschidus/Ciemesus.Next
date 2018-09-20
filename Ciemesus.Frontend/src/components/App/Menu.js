import React from 'react';
import { Link } from 'react-router-dom';

const Menu = () => (
    <nav className="Menu">
        <Link to="/fika"><span className="Menu-link fa fa-birthday-cake" /></Link>
        <Link to="/scheduler"><span className="Menu-link fa fa-calendar-check-o" /></Link>
        <Link to="/profile"><span className="Menu-link fa fa-user" /></Link>
    </nav>
);

export default Menu;
