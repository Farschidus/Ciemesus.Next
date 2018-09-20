import React from 'react';

const AuthenticationContext = React.createContext({
    token: null,
    sessionExpired: true,
    isAuthenticated: false,
    authenticate: () => {},
});

export const AuthenticationConsumer = AuthenticationContext.Consumer;

export class AuthenticationProvider extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            token: null,
            sessionExpired: false,
            isAuthenticated: true,
        };

        this.authenticate = this.authenticate.bind(this);
    }

    authenticate(_token) {
        this.setState({
            token: _token,
            isAuthenticated: true,
            sessionExpired: false,
        });
    }

    render() {
        return (
            <AuthenticationContext.Provider value={{
                token: this.state.token,
                isAuthenticated: this.state.isAuthenticated,
                authenticate: this.authenticate,
                sessionExpired: this.state.sessionExpired,
            }}
            >
                {this.props.children}
            </AuthenticationContext.Provider>
        );
    }
}
