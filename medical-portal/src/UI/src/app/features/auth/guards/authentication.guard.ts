import { Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AuthGuard } from './abstract-auth.guard';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export abstract class AuthenticationGuard extends AuthGuard {
  public constructor(
    protected override authService: AuthService,
    private router: Router
  ) {
    super(authService);
  }

  protected handleAccessCheckOLD(
    routeRedirect: string | undefined
  ): (authenticated: boolean) => boolean | UrlTree {
    return (authenticated: boolean): boolean | UrlTree =>
      authenticated && this.authService.hasAccess()
        ? true
        : this.router.createUrlTree([routeRedirect ?? '/'], {
            queryParams:
              this.router.getCurrentNavigation()?.extractedUrl.queryParams,
            queryParamsHandling: 'merge',
          });
  }

  protected handleAccessCheck(
    routeRedirect: string | undefined
  ): Observable<boolean | UrlTree> {
    console.log('handle access...');
      return this.authService.hasAccess().pipe(map((hasAccess) => {
        console.log('hasAccess', hasAccess);
        return hasAccess
          ? true
          : this.router.createUrlTree([routeRedirect ?? '/'], {
              queryParams:
                this.router.getCurrentNavigation()?.extractedUrl.queryParams,
              queryParamsHandling: 'merge',
            });
      }));
  }

  protected handleAccessError(): boolean {
    return false;
  }
}
