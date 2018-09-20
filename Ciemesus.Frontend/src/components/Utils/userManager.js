import { UserManager } from 'oidc-client';
import config from './config';

const userManager = new UserManager({
    client_id: config.identityServer.clientId,
    client_secret: config.identityServer.clientSecret,
    redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port
        ? `:${window.location.port}` : ''}/signin-oidc`,
    post_logout_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port
        ? `:${window.location.port}` : ''}/signout-oidc`,
    response_type: 'token id_token',
    scope: 'offline_access profile openid email Fika.Api.Read',
    authority: config.authority,
    silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port
        ? `:${window.location.port}` : ''}/silent-renew`,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true,
});

export default userManager;
