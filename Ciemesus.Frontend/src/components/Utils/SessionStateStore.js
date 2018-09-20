import { observable, computed } from 'mobx';
import { create, persist } from 'mobx-persist';
import { browserHistory } from 'react-router';

class SessionStateStore {
    @observable isLoading = true;

    @persist('object') @observable _token = {};
    @persist @observable redirectBackTo = '/';
    @observable sessionExpired = false;

    get token() {
        return this._token;
    }

    set token(token) {
        if (Object.keys(token).length !== 0) {
            this.sessionExpired = false;
        }

        this._token = token;
    }

    @computed get isAuthenticated() {
        return this.token.access_token !== undefined;
    }

    logout(sessionExpired = false, redirectToLogin = false) {
        if (Object.keys(this.token).length === 0) {
            return;
        }

        this.sessionExpired = sessionExpired;
        this.token = {};

        if (this.sessionExpired === true) {
            this.redirectBackTo = `${window.location.pathname}${window.location.hash || ''}`;

            if (redirectToLogin === true) {
                setTimeout(() => {
                    browserHistory.push('/login');
                }, 0);
            }
        } else {
            this.redirectBackTo = '/';
        }
    }
}

const hydrate = create({});
const sessionStateStore = new SessionStateStore();

hydrate('sessionStateStore', sessionStateStore)
    .then(() => {
        sessionStateStore.isLoading = false;
    });

export default sessionStateStore;
