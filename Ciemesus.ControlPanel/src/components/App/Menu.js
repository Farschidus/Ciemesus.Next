import React from 'react';
import { NavLink } from 'react-router-dom';

class Menu extends React.Component {
    constructor(props) {
        super(props);
        this.onItemClick = this.onItemClick.bind(this);
    }

    onItemClick() {

    }

    render() {
        return (
            <nav className="Menu">
                <NavLink activeClassName="Menu-link--active" exact to="/"><span className="Menu-link fa fa-user" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/settings"><span className="Menu-link fa fa-trophy" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/languages"><span className="Menu-link fa fa-calendar-check-o" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/pages"><span className="Menu-link fa fa-birthday-cake" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/groupPages"><span className="Menu-link fa fa-birthday-cake" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/gallery"><span className="Menu-link fa fa-birthday-cake" /></NavLink>
                <NavLink activeClassName="Menu-link--active" to="/localization"><span className="Menu-link fa fa-birthday-cake" /></NavLink>
            </nav>
        );
    }
}

export default Menu;
