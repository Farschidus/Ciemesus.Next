import React from 'react';
import PropTypes from 'prop-types';
import Months from './../Utils/Months';
import Calendar from 'react-calendar';

class ScheduleMember extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isActive: false,
        };

        this.toggleItem = this.toggleItem.bind(this);
    }

    toggleItem() {
        this.setState({
            isActive: !this.state.isActive,
        });
    }

    pad(number) {
        return (`0${number}`).slice(-2);
    }

    render() {
        const profilePhoto = {
            backgroundImage: `url(${this.props.memberImage})`,
        };

        const date = `${this.props.fikaDate.getDate()} ${Months[this.props.fikaDate.getMonth()]}`;
        const time = `${this.pad(this.props.fikaDate.getHours())}:${this.pad(this.props.fikaDate.getMinutes())}`;

        const classNames = this.state.isActive ? 'Scheduler-details' : 'Scheduler-details Collapsed';

        return (
            <div className="Scheduler-item" onClick={this.toggleItem} role="button" >
                <span className="Scheduler-profilePhoto" style={profilePhoto} />
                <span className="Scheduler-title">{this.props.fikaTitle}</span>
                <span className="Scheduler-personName">{this.props.memberName}</span>
                <span className="Scheduler-dateTime">
                    <span className="Scheduler-date">{date}</span>
                    <span className="Scheduler-time">{time}</span>
                </span>
                <div className={classNames}>
                    <Calendar
                        onChange={this.onChange}
                        value={this.props.fikaDate}
                        activeStartDate={new Date().now}
                    />
                </div>
            </div>
        );
    }
}

ScheduleMember.propTypes = {
    fikaTitle: PropTypes.string,
    memberName: PropTypes.string,
    memberImage: PropTypes.string,
    fikaDate: PropTypes.instanceOf(Date),
};

export default ScheduleMember;
