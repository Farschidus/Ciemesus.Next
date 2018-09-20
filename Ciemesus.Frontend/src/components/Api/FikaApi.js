import BaseApi from './BaseApi';

class FikaApi extends BaseApi {
    apiBase = 'fika';

    getFika(fikaId) {
        return this._get(`${this.apiBase}/getFika/${fikaId}`);
    }

    getFikaList(teamId) {
        return this._get(`${this.apiBase}/GetFikaList/${teamId}`);
    }

    postFikaImage(fikaId, body) {
        return this._post(`${this.apiBase}/postImage`, body);
    }
}

export default new FikaApi();
