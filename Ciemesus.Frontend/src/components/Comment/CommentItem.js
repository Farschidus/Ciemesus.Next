import React from 'react';
import PropTypes from 'prop-types';
import TimeAgo from 'react-timeago';

class CommentItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            liked: false,
            likes: this.props.likes,
        };

        this.ToggleLike = this.ToggleLike.bind(this);
    }

    ToggleLike() {
        const likeCount = this.state.liked ? this.state.likes - 1 : this.state.likes + 1;

        this.setState({
            likes: likeCount,
            liked: !this.state.liked,
        });
    }

    ConvertUTCDateToLocalDate(date) {
        const newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

        const offset = date.getTimezoneOffset() / 60;
        const hours = date.getHours();

        newDate.setHours(hours - offset);

        return newDate;
    }

    render() {
        const profilePhoto = {
            backgroundImage: `url(${this.props.memberPic})`,
        };

        const comentLikes = this.state.likes;
        const commentLikeClass = this.state.liked ? 'Comment-like Comment-like--active LinkButton' : 'Comment-like LinkButton';
        const commentDate = this.ConvertUTCDateToLocalDate(this.props.commentDate);

        return (
            <span className="Comment" data-id={this.props.commentId}>
                <span className="Comment-profilePhoto" style={profilePhoto} />
                <span className="Comment-body">
                    <span className="Comment-title">{this.props.memberName}</span>
                    <span className="Comment-date">
                        <TimeAgo date={commentDate} />
                    </span>
                    <span className="Comment-comment">{this.props.comment}</span>
                    <button className={commentLikeClass} onClick={this.ToggleLike}>{comentLikes} likes</button>
                </span>
            </span>
        );
    }
}

CommentItem.propTypes = {
    commentId: PropTypes.number.isRequired,
    memberPic: PropTypes.string,
    memberName: PropTypes.string.isRequired,
    comment: PropTypes.string.isRequired,
    commentDate: PropTypes.instanceOf(Date).isRequired,
    likes: PropTypes.number.isRequired,
};

export default CommentItem;
