import React from 'react';
import PropTypes from 'prop-types';
import { Transition } from 'react-transition-group';

class FikaItem extends React.Component {
    constructor(props) {
        super();
        this.state = {
            id: props.id,
            name: props.name,
            date: props.date,
            title: props.title,
            imageUrl: `./../../clientImages/fikas/${props.image}`,
        };
    }

    onError() {
        this.setState({
            imageUrl: './../../clientImages/profile/avatar.png',
        });
    }

    render() {
        const fikaStyle = {
            backgroundImage: `url(${require('./../../clientImages/fikas/1.jpg')})`,
        };

        return (
            <div className="FikaWrapper">
                <span className="Fika" style={fikaStyle}>
                    <span className="Fika-person">{this.state.name}</span>
                    <span className="Fika-title">{this.state.title}</span>
                    <span className="Fika-date">{this.state.date}</span>
                </span>
            </div>
        );
    }
}

FikaItem.propTypes = {
    id: PropTypes.number.isRequired,
    name: PropTypes.string.isRequired,
    date: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    image: PropTypes.string,
};

export default FikaItem;
