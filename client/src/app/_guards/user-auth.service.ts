import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {AuthService} from "../_services/auth.service";
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class UserAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {
  }

  canActivate(): boolean {
    if (this.authService.isLoggedIn() && !this.authService.isAdmin()) {
      return true;
    }
    this.toastr.error('Please Login!');
    this.router.navigate(['login']);
    return false;
  }

}
