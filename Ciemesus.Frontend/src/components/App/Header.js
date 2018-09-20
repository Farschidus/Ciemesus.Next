import React from 'react';
import { Link } from 'react-router-dom';
import FikaLogo from './../../assets/images/TextLogo.png';

const stylus = { margin: '-53px 0 0 20px', position: 'absolute', fontSize: '30px' };
const Header = () => (
    <div className="Header">
        <img src={FikaLogo} alt="" width="60" height="31" className="Fika-logo--top" />
        <Link to="/fika" className="RoutLink"><span style={stylus} className="fa fa-angle-left" /></Link>
    </div>
);

export default Header;
