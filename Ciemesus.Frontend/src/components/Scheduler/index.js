import React from 'react';
import { Link } from 'react-router-dom';
import FikaApi from './../Api/FikaApi';

class Scheduler extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            items: null,
        };
    }

    componentDidMount() {
        FikaApi.getFikaList(1).then((result) => {
            this.setState({ items: result });
        });
    }

    render() {
        const profilePhoto = {
            backgroundImage: `url(/clientImages/profiles/farschidus.jpg)`
        };

        return (
            <div className="Scheduler">
                <div className="Scheduler-item">
                    <span className="Scheduler-profilePhoto" style={profilePhoto} />
                    <span className="Scheduler-title">This is a title</span>
                    <span className="Scheduler-personName">Sebastian Kondratowicz</span>
                    <span className="Scheduler-dateTime">
                        <span className="Scheduler-date">12 Dec</span>
                        <span className="Scheduler-time">15:00</span>
                    </span>
                    <div className="Scheduler-details Collapsed">
                        Calendar
                    </div>
                </div>
                <div className="Scheduler-item">
                    <span className="Scheduler-profilePhoto" style={profilePhoto} />
                    <span className="Scheduler-title">This is a title</span>
                    <span className="Scheduler-personName">Sebastian Kondratowicz</span>
                    <span className="Scheduler-dateTime">
                        <span className="Scheduler-date">12 Dec</span>
                        <span className="Scheduler-time">15:00</span>
                    </span>
                    <div className="Scheduler-details Collapsed">
                        Calendar
                    </div>
                </div>
                <div className="Scheduler-item">
                    <span className="Scheduler-profilePhoto" style={profilePhoto} />
                    <span className="Scheduler-title">This is a title</span>
                    <span className="Scheduler-personName">Sebastian Kondratowicz</span>
                    <span className="Scheduler-dateTime">
                        <span className="Scheduler-date">12 Dec</span>
                        <span className="Scheduler-time">15:00</span>
                    </span>
                    <div className="Scheduler-details Collapsed">
                        Calendar
                    </div>
                </div>                
            </div>
        );
    }
}

export default Scheduler;
