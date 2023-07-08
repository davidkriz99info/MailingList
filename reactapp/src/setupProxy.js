const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/MailingList",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7041',
        secure: false
    });

    app.use(appProxy);
};
