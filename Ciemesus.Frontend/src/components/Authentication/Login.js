import React from 'react';
import LoginLogo from './../../assets/images/LoginLogo.png';
import { Link } from 'react-router-dom';
import LoadingButton from './../LoadingButton';

class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            failedAttempts: 0,
        };
    }

    render() {
        return (
            <div className="full-height">
                <section className="Login">
                    <img className="Login-logo u-centered" src={LoginLogo} width="150" alt="Fika logo" />
                    <div className="Login-form">
                        <div className="Login-formGroup">
                            <Link className="Login-formGroupLink Link u-textRight u-block" to="/forgottenPassword">Forgotten Password</Link>
                        </div>
                        <div className="Login-formGroup">
                            <input className="Login-formGroupField Textbox" placeholder="Email" type="Text" />
                        </div>
                        <div className="Login-formGroup">
                            <input className="Login-formGroupField Textbox" placeholder="Password" type="Password" />
                        </div>
                        <div className="Login-formGroup">
                            <LoadingButton />
                        </div>

                        <span className="Divider">OR</span>

                        <div className="Login-formGroup">
                            <Link className="Button Button--secondary" to="/register">Create Account</Link>
                        </div>
                    </div>
                </section>
            </div>
        );
    }
}

export default Login;
