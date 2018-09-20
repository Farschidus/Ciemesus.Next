import objectAssignDeep from './objectAssignDeep';
import settings from './../../../config.json';

const appEnv = process.env.APP_ENV || 'development';

export function getConfig(env) {
    // eslint-disable-next-line global-require
    let config = settings;

    let userConfig = {};
    try {
        // eslint-disable-next-line global-require, import/no-unresolved
        userConfig = require('./../../config.user.json');
    } catch (ex) {
        // no user config
    }

    const common = config.common;
    config = objectAssignDeep(common, objectAssignDeep(config, userConfig)[env]);
    config.proj = config.proj;

    return config || {};
}

export default appEnv ? getConfig(appEnv) : {};
