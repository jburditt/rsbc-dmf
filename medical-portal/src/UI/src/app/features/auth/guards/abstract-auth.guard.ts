import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  UrlTree,
} from '@angular/router';
import { Observable, concat, catchError, map, of, tap } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ROUTE_DENIED } from '@app/app.routes';

export abstract class AuthGuard
  implements CanActivate, CanActivateChild, CanLoad
{
  public constructor(protected authService: AuthService) {}

  public canActivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.checkAccess(ROUTE_DENIED);
  }

  public canActivateChild(
    childRoute: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.checkAccess(ROUTE_DENIED);
  }

  public canLoad(
    route: Route
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.checkAccess(ROUTE_DENIED);
  }

  protected checkAccess(
    routeRedirect: string | undefined
  ): Observable<boolean | UrlTree> {
    console.log('Checking access...');
    return this.authService.isLoggedIn().pipe(
      tap((authenticated) => {
        console.log('Authenticated: ', authenticated);
        authenticated
          ? this.handleAccessCheck(routeRedirect)
          : of(false)
      }),
      catchError((error) => {
        console.error('Error occurred during access validation: ', error);
        return of(this.handleAccessError());
      })
    );
  }

  /**
   * @description
   * Hook for checking whether the user is authenticated and
   * should be provided access or redirected.
   */
  protected abstract handleAccessCheck(
    routeRedirect: string | undefined
  ): Observable<boolean | UrlTree>;

  /**
   * @description
   * Hook for determining access or not based on authentication
   * failing for an unknown reason.
   */
  protected abstract handleAccessError(): boolean;
}
