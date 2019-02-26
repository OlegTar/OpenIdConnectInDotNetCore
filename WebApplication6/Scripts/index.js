"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const oidc_client_1 = require("oidc-client");
var settings = {
    authority: 'https://accounts.google.com',
    client_id: '762574578671-0fieldv302nufou2afqfu4tgt0nopdtq.apps.googleusercontent.com',
    redirect_uri: 'https://localhost:44389/Home2/',
    response_type: 'id_token token',
    scope: 'openid email profile',
    filterProtocolClaims: true,
    loadUserInfo: true
};
let client = new oidc_client_1.OidcClient(settings);
client.createSigninRequest({ state: "abc" }).then(req => {
    document.getElementById('login').setAttribute('href', req.url);
}).catch(function (err) {
    console.log(err);
});
let signinResponse;
if (window.location.href.indexOf('#') !== -1) {
    client.processSigninResponse().then(function (response) {
        signinResponse = response;
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + signinResponse.access_token);
        var myInit = {
            method: 'GET',
            headers: myHeaders
        };
        fetch('https://openidconnect.googleapis.com/v1/userinfo', myInit).then(res => {
            return res.text();
        }).then(text => {
            let o = JSON.parse(text);
            document.getElementById('user').innerHTML = o.name;
        }).catch(err => {
            console.log(err);
        });
    }).catch(function (err) {
        console.log(err);
    });
}
//# sourceMappingURL=index.js.map