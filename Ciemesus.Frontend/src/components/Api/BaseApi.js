import queryString from 'query-string';
import sessionStateStore from './../Utils/SessionStateStore';

export default class BaseApi {
    baseUrl;

    constructor() {
        this.baseUrl = 'http://localhost:58751/api/';
    }

    async _get(endpoint, params = {}) {
        const result = await this._request({
            endpoint,
            method: 'GET',
            params,
        });
        return result;
    }

    async _put(endpoint, body, params = {}) {
        const result = await this._request({
            endpoint,
            method: 'PUT',
            params,
            body,
        });
        return result;
    }

    async _post(endpoint, body, params = {}) {
        const result = await this._request({
            endpoint,
            method: 'POST',
            params,
            body,
        });
        return result;
    }

    async _delete(endpoint, body, params = {}) {
        const result = await this._request({
            endpoint,
            method: 'DELETE',
            body,
            params,
        });
        return result;
    }

    _request({
        endpoint,
        method,
        params = {},
        body = {},
    }) {
        const stringifiedParams = Object.keys(params).length !== 0 &&
                params.constructor === Object
            ? `?${queryString.stringify(params)}`
            : '';
        const url = `${this.baseUrl}${endpoint}${stringifiedParams}`;

        const token = sessionStateStore.token.access_token;
        const headers = new Headers();
        const options = {
            method,
            headers,
        };

        headers.append('Authorization', `Bearer ${token}`);

        if (method === 'POST') {
            options.body = body;
        } else {
            headers.append('Content-Type', 'application/json');
            if (['get', 'head'].indexOf(method.toLowerCase()) === -1) {
                options.body = JSON.stringify(body);
            }
        }

        return fetch(url, options)
            .then((result) => {
                if (result.status >= 400) {
                    if (result.status === 401) {
                        sessionStateStore.logout(true, true);
                    }

                    return result.text().then((text) => { throw (JSON.parse(text)); });
                }

                if (result.status < 300 && result.status >= 200) {
                    return result.text().then((text) => (text ? JSON.parse(text) : {}));
                }

                return {};
            });
    }
}
