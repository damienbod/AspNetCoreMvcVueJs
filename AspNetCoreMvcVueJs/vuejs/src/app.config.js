let config = {
    stsServerIdentityUrl: 'https://localhost:44348',
    ApiUrl: 'https://localhost:44341/api/DataEventRecords/',
    client_id: 'vuejs_code_client',
    redirect_uri: 'https://localhost:44341/callback.html',
    silent_redirect_uri: 'https://localhost:44341/silent-renew.html',
    post_logout_redirect_uri: 'https://localhost:44341/',
};
if (process.env.VUE_APP_MODE === 'production') {
    config = {
        stsServerIdentityUrl: 'https://localhost:44348',
        ApiUrl: 'https://localhost:44341/api/DataEventRecords/',
        client_id: 'vuejs_code_client',
        redirect_uri: 'https://localhost:44341/callback.html',
        silent_redirect_uri: 'https://localhost:44341/silent-renew.html',
        post_logout_redirect_uri: 'https://localhost:44341/',
    };
}
export default config;
//# sourceMappingURL=app.config.js.map