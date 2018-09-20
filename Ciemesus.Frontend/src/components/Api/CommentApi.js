import BaseApi from './BaseApi';

class CommentApi extends BaseApi {
    apiBase = 'comment';

    GetCommentList(fikaId) {
        return this._get(`${this.apiBase}/GetCommentList/${fikaId}`);
    }

    AddComment(body) {
        return this._post(`${this.apiBase}/AddComment/`, null, body);
    }
}

export default new CommentApi();
