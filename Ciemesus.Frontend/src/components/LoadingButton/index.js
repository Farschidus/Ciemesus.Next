import React from 'react';
import { Redirect } from 'react-router';
// import { AuthenticationConsumer } from './../Utils/Authentication';

class LoadingButton extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            buttonState: 0,
        };

        this.click = this.click.bind(this);
    }

    click() {
        this.setState({ buttonState: 1 });

        window.setTimeout(() => {
            this.setState({ buttonState: 2 });
        }, 600);

        window.setTimeout(() => {
            this.setState({ redirect: true });
        }, 1500);

        // store.authenticate('SignedIn');
        // console.log(store);
    }

    render() {
        const { redirect } = this.state;

        if (redirect) {
            return <Redirect to="/fika" />;
        }

        let className;
        let buttonText;

        switch (this.state.buttonState) {
            case 1:
                className = 'Button Button--primary is-animating';
                break;
            case 2:
                className = 'Button Button--primary is-animated';
                buttonText = 'Loading';
                break;
            default:
                className = 'Button Button--primary with-animation';
                buttonText = 'Log in';
                break;
        }
        return (
            <span>
                <button className={className} onClick={this.click}>{buttonText}</button>
            </span>
            /* <AuthenticationConsumer>
                { (store) => (
                    <span>
                        <button className={className} onClick={this.click(store)}>{buttonText}</button>
                    </span>)
                }
            </AuthenticationConsumer> */
        );
    }
}

export default LoadingButton;
