import React from 'react';
import LoginLogo from '../../assets/images/LoginLogo.png';
import { Link } from 'react-router-dom';

class ForgottenPassword extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            failedAttempts: 0,
        };
    }

    render() {
        return (
            <section className="Login">
                <img className="Login-logo u-centered" src={LoginLogo} width="150" alt="Fika logo" />
                <div className="Login-form">
                    <div className="Login-formGroup">
                        <Link className="Login-formGroupLink Link u-textRight u-block" to="/login">Cancel</Link>
                    </div>
                    <div className="Login-formGroup">
                        <input className="Login-formGroupField Textbox" placeholder="Email" type="Text"/>
                    </div>
                    <div className="Login-formGroup">
                        <a className="Button Button--primary">Recover</a>
                    </div>
                    <span className="Divider-full"></span>   
                </div>
            </section>
        );
    }
}

export default ForgottenPassword;
