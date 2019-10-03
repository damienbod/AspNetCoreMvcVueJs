import { UserManager, WebStorageStateStore } from 'oidc-client';
export default class AuthService {
    constructor() {
        const STS_DOMAIN = 'https://localhost:44348';
        const settings = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            authority: STS_DOMAIN,
            client_id: 'vuejs_code_client',
            redirect_uri: 'https://localhost:44341/callback.html',
            automaticSilentRenew: true,
            silent_redirect_uri: 'https://localhost:44341/silent-renew.html',
            response_type: 'code',
            scope: 'openid profile dataEventRecords',
            post_logout_redirect_uri: 'https://localhost:44341/',
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