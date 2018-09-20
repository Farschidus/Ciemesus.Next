import React from 'react';
import TeamApi from './../Api/TeamApi';
import ScheduleMember from './ScheduleMember';

class ScheduleList extends React.Component {
    constructor() {
        super();

        this.state = {
            teamName: null,
            members: null,
            interval: null,
            dayOfWeek: null,
        };
    }

    componentDidMount() {
        TeamApi.getSchedul(1).then((result) => {
            this.setState({
                teamName: result.teamName,
                members: result.members,
                interval: result.interval,
                dayOfWeek: result.dayOfWeek,
            });
        });
    }

    render() {
        if (this.state.members === null) {
            return <div>LOADING</div>;
        }

        return (
            <div className="Scheduler">
                <div className="tempYellowButton">{this.state.teamName}</div>
                <div className="tempNavyButton">{this.state.interval}</div>
                <div className="tempNavyButton">{this.state.dayOfWeek}</div>
                <br /><br /><br />
                {this.state.members.map((item, i) => (
                    <ScheduleMember
                        key={i}
                        memberName={item.memberName}
                        memberImage={item.pic}
                        fikaTitle={item.fikaTitle}
                        fikaDate={new Date(item.fikaDate)}
                    />
                ))}
            </div>
        );
    }
}

export default ScheduleList;
