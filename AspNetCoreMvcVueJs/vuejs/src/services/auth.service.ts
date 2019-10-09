import { UserManager, WebStorageStateStore, User } from 'oidc-client';
import config from '@/app.config';

export default class AuthService {
    private userManager: UserManager;

    constructor() {
        const STS_DOMAIN: string = config.stsServerIdentityUrl;

        const settings: any = {
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

    public getUser(): Promise<User | null> {
        return this.userManager.getUser();
    }

    public login(): Promise<void> {
        return this.userManager.signinRedirect();
    }

    public logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }

    public getAccessToken(): Promise<string> {
        return this.userManager.getUser().then((data: any) => {
            return data.access_token;
        });
    }
}
