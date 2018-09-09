import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../alertify.service';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private auth: AuthService, private router: Router, private alert: AlertifyService) {}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {

    if (this.auth.loggedIn()) {
      return true;
    }

    this.alert.error('You shall not enter via this route!!!');
    this.router.navigate(['/home']);
    return false;

  }
}
