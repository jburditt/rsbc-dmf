import { Injectable } from '@angular/core';
import { Observable, from, map, of } from 'rxjs';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakLoginOptions } from 'keycloak-js';
import { Role } from '../enums/identity-provider.enum';

export interface IAuthService {
  login(options?: KeycloakLoginOptions): Observable<void>;
  isLoggedIn(): Observable<boolean>;
  logout(redirectUri: string): Observable<void>;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService
{
  public constructor(private keycloakService: KeycloakService, private profileManagementService: ProfileManagementService) {}

  public login(options?: KeycloakLoginOptions): Observable<void> {
    return from(this.keycloakService.login(options));
  }

  public getHpdid(): string | undefined {
    const idTokenParsed = this.keycloakService.getKeycloakInstance().idTokenParsed;
    if (idTokenParsed !== undefined) {
      return idTokenParsed['preferred_username'];
    }
    return undefined;
  }

  public isLoggedIn(): Observable<boolean> {
    return of(this.keycloakService.isLoggedIn());
  }

  public hasAccess(): Observable<boolean> {
    //console.info('getUserRoles', this.keycloakService.getUserRoles());
    // check role from Keycloak directly
    //return this.keycloakService.isLoggedIn() && this.keycloakService.isUserInRole(Role.Enrolled);


    if (true || !this.keycloakService.isLoggedIn())
      return of(false);
    console.log("getRoles")
    return this.getRoles().pipe(map((roles) => {
      console.log("TEST");
      console.log("has role", roles.includes(Role.Enrolled));
      return roles.includes(Role.Enrolled);
    }));
  }

  // get roles directly from Keycloak
  // public getRoles(): Role[] {
  //   const roleNames = this.keycloakService
  //     .getUserRoles()
  //     .filter((role) => role !== Role.Enrolled);
  //   return roleNames
  //     .map((role) => Object.values(Role).find((value) => value === role))
  //     .filter((role) => role !== undefined) as Role[];
  // }

  public getRoles(): Observable<Role[]>
  {
    console.log("getRoles 2")
    return of([Role.Practitioner] as Role[]);
    // return this.profileManagementService.getCachedProfile().pipe(map((profile) => {
    //   console.log("profile", profile);
    //   if (!profile.roles) return [];
    //   return profile.roles
    //     .map((role) => Object.values(Role).find((value) => value === role))
    //     .filter((role) => role !== undefined) as Role[];
    // }));
  }

  public logout(redirectUri: string): Observable<void> {
    return from(this.keycloakService.logout(redirectUri));
  }
}
