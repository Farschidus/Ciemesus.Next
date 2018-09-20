import React from 'react';
import PropTypes from 'prop-types';
import Months from './../Utils/Months';
import CommentItem from './../Comment/CommentItem';
import AddComment from './../Comment/AddComment';

class FikaComments extends React.Component {
    constructor(props) {
        super(props);

        const fikaObj = {
            id: props.fikaObj.fikaId,
            name: props.fikaObj.fikaName,
            memberName: props.fikaObj.fikaMemberName,
            memberPic: { backgroundImage: `url(${props.fikaObj.fikaMemberPic})` },
            pic: props.fikaObj.fikaPic.length > 0 ? { backgroundImage: `url(${props.fikaObj.fikaPic})` } : '',
            date: new Date(props.fikaObj.fikaDate),
            likes: props.fikaObj.likes,
        };

        const commentsObj = props.fikaObj.comments.map((obj) => {
            const newObj = Object.assign({}, obj);

            newObj.commentDate = new Date(newObj.commentDate);
            return newObj;
        });

        this.state = {
            fika: fikaObj,
            comments: commentsObj,
        };

        this.UpdateComments = this.UpdateComments.bind(this);
        this.ConvertDateToUTC = this.ConvertDateToUTC.bind(this);
    }

    ConvertDateToUTC(date) {
        return new Date(
            date.getUTCFullYear(),
            date.getUTCMonth(),
            date.getUTCDate(),
            date.getUTCHours(),
            date.getUTCMinutes(),
            date.getUTCSeconds(),
        );
    }

    UpdateComments(comment) {
        const newComment = comment;
        const newComments = this.state.comments;

        newComment.commentDate = this.ConvertDateToUTC(new Date(newComment.commentDate));

        newComments.push(newComment);
        this.setState({
            comments: newComments,
        });
    }

    render() {
        if (this.state.comments === null) {
            return <div>LOADING</div>;
        }

        return (
            <div className="Fika">
                <div className="FikaWithComments">
                    <span className="FikaWithComments-date">
                        {this.state.fika.date.getDate()}<br />{Months[this.state.fika.date.getMonth()]}
                    </span>
                    <span className="FikaWithComments-profilePhoto" style={this.state.fika.memberPic} />
                    <span className="FikaWithComments-personName">{this.state.fika.memberName}</span>
                    <span className="FikaWithComments-stats">
                        {this.state.fika.likes} likes
                    </span>

                    {typeof this.state.fika.pic === 'object' &&
                        <span className="FikaWithComments-imageSlider">
                            <span className="ImageSlider" style={this.state.fika.pic}>
                                <span className="ImageSlider-background" />
                            </span>
                        </span>
                    }
                    {this.state.comments.map((item, i) => (
                        <CommentItem
                            key={i}
                            commentId={item.fikaCommentId}
                            memberName={item.memberName}
                            memberPic={item.memberPic}
                            comment={item.memberComment}
                            commentDate={item.commentDate}
                            likes={item.likes}
                        />
                    ))}
                    <AddComment fikaId={this.state.fika.id} addComment={this.UpdateComments} />
                </div>

                <span className="FikaWithComments-empty" />
            </div>
        );
    }
}

FikaComments.propTypes = {
    fikaObj: PropTypes.object.isRequired,
};

export default FikaComments;
