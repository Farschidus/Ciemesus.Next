import React from 'react';
import FikaApi from './../Api/FikaApi';
import FikaComments from './FikaComments';

class FikaList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            teamId: null,
            fikas: null,
        };
    }

    componentDidMount() {
        FikaApi.getFikaList(1).then((results) => {
            this.setState({ teamId: results.teamId, fikas: results.fikas });
        });
    }

    render() {
        if (this.state.fikas === null) {
            return <div>LOADING</div>;
        }

        return (
            <div className="Fikas">
                {this.state.fikas.map((fika, i) => (
                    <FikaComments
                        key={i}
                        fikaObj={fika}
                    />
                ))}
            </div>
        );
    }
}

export default FikaList;
