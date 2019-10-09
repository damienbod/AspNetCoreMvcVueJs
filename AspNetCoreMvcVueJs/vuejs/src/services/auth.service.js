import { UserManager, WebStorageStateStore } from 'oidc-client';
import config from '@/app.config';
export default class AuthService {
    constructor() {
        const STS_DOMAIN = config.stsServerIdentityUrl;
        const settings = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            authority: STS_DOMAIN,
            client_id: config.client_id,
            redirect_uri: config.redirect_uri,
            automaticSilentRenew: true,
            silent_redirect_uri: config.silent_redirect_uri,
            response_type: 'code',
            scope: 'openid profile dataEventRecords',
            post_logout_redirect_uri: config.post_logout_redirect_uri,
            filterProtocolClaims: true,
        };
        this.userManager = new UserManager(settings);
    }
    getUser() {
        return this.userManager.getUser();
    }
    login() {
        return this.userManager.signinRedirect();
    }
    logout() {
        return this.userManager.signoutRedirect();
    }
    getAccessToken() {
        return this.userManager.getUser().then((data) => {
            return data.access_token;
        });
    }
}
//# sourceMappingURL=auth.service.js.map