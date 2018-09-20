import React from 'react';
import PropTypes from 'prop-types';
import CommentApi from './../Api/CommentApi';
import FikaApi from './../Api/FikaApi';

class AddComment extends React.Component {
    constructor(props) {
        super(props);

        this.fileInput = React.createRef();
        this.commentInput = React.createRef();
        this.btnImageUploader = React.createRef();
        this.sendComment = this.sendComment.bind(this);
        this.keyDown = this.keyDown.bind(this);
        this.fileChangedHandler = this.fileChangedHandler.bind(this);
    }

    sendComment() {
        if (!this.commentInput.current.value) {
            return;
        }

        const val = this.commentInput.current.value;
        const body = {
            Comment: val,
            FikaId: this.props.fikaId,
            MemberId: 3,
        };

        CommentApi.AddComment(body).then((result) => {
            this.commentInput.current.value = '';
            this.props.addComment(result);
        });
    }

    keyDown(e) {
        if (e.keyCode === 13) {
            this.sendComment();
        }
    }

    fileChangedHandler(event) {
        const imageFile = event.target.files[0];
        if (window.FormData !== undefined) {
            if (imageFile.type.includes('image')) {
                const fd = new FormData();
                fd.append('FikaId', this.props.fikaId);
                fd.append('ImageFile', imageFile);
                FikaApi.postFikaImage(this.props.fikaId, fd).then(() => {
                    this.btnImageUploader.className = `${this.btnImageUploader.className} hidden`;
                });
            } else {
                alert('Upload image');
            }
        } else {
            alert('not support');
        }
    }

    render() {
        return (
            <span className="FikaWithComments-addComment">
                <input
                    name="ImageFile"
                    type="file"
                    onChange={this.fileChangedHandler}
                    className="hidden"
                    ref={(fileInput) => this.fileInput = fileInput}
                />
                <button
                    className="fa fa-camera LinkButton FikaWithComments-sendPhoto"
                    onClick={() => this.fileInput.click()}
                    ref={(btnImageUploader) => this.btnImageUploader = btnImageUploader}
                />
                <input className="FikaWithComments-addCommentTextbox" type="text" ref={this.commentInput} onKeyDown={this.keyDown} />
                <button className="fa fa-send LinkButton FikaWithComments-sendComment" onClick={this.sendComment} />
            </span>
        );
    }
}

AddComment.propTypes = {
    fikaId: PropTypes.number.isRequired,
    addComment: PropTypes.func.isRequired,
};

export default AddComment;
