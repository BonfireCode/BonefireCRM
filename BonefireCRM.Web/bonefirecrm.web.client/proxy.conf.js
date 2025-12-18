const { env } = require('process');

const aspireTarget =
  env["services__apiservice__https__0"]
  || env["services__apiservice__http__0"];

const manualTarget = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(';')[0]
    : 'https://localhost:7541';

const target = aspireTarget || manualTarget;

const secure = env.NODE_ENV
  ? env["NODE_ENV"] !== "development"
  : false

const PROXY_CONFIG = [
  {
    context: [
      "/api/**", "/weatherforecast"
    ],
    target: target,
    secure: secure,
  }
]

module.exports = PROXY_CONFIG;
