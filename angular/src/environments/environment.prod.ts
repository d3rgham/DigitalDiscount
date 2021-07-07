import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'DigitalDiscounts',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44313',
    redirectUri: baseUrl,
    clientId: 'DigitalDiscounts_App',
    responseType: 'code',
    scope: 'offline_access DigitalDiscounts',
  },
  apis: {
    default: {
      url: 'https://localhost:44313',
      rootNamespace: 'DigitalDiscounts',
    },
  },
} as Environment;
