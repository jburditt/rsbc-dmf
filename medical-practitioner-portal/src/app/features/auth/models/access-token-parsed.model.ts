import { KeycloakResourceAccess, KeycloakTokenParsed } from 'keycloak-js';

import { IdentityProvider } from '../enums/identity-provider.enum';

export interface AccessTokenParsed extends KeycloakTokenParsed {
  acr: string;
  'allowed-origins': string[];
  aud: string;
  auth_time: number;
  azp: string;
  iss: string;
  jti: string;
  scope: string;
  typ: string;
  sub: string;
  // User specific attributes:
  identity_provider: IdentityProvider;
  resource_access: KeycloakResourceAccess;
  email_verified: boolean;
  family_name: string;
  given_name: string;
  email: string;
  pidp_email: string;
  name: string;
  preferred_username: string;
  birthdate: string;
}
