import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Router} from '@angular/router';
import {ToastrService} from "ngx-toastr";


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router, private toast: ToastrService) {
  }

  login(loginModel: any): Observable<boolean> {
    return this.http.post(environment.url + '/user/login', loginModel).pipe(map(res => {
      if (res) {
        // @ts-ignore
        localStorage.setItem('token', res.token);
        this.toast.success("Welcome Back! :)")
        return true;
      }
      return false;
    }));
  }

  adminLogin(loginModel: any): Observable<boolean> {
    return this.http.post(environment.url + '/admin/login', loginModel).pipe(map(res => {
      if (res) {
        // @ts-ignore
        localStorage.setItem('token', res.token);
        this.toast.success("Welcome Back! :)")
        return true;
      }
      return false;
    }));
  }

  logout() {
    localStorage.removeItem('token');
    this.toast.show("Bye bye! :(")
    this.router.navigate(['']);

  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (token == null || !token) {
      return false;
    }
    const jwtHelper = new JwtHelperService();
    return !jwtHelper.isTokenExpired(token);
  }

  isAdmin(): boolean {
    if (!this.isLoggedIn()) {
      return false;
    }
    const token = localStorage.getItem('token');
    const jwtHelper = new JwtHelperService();
    // @ts-ignore
    const tokenDecoded = jwtHelper.decodeToken(token)
    return tokenDecoded['role'] === 'Admin' || tokenDecoded['role']==='Admin2';
  }

  isAdmin2() {
    if (!this.isLoggedIn()) {
      return false;
    }
    const token = localStorage.getItem('token');
    const jwtHelper = new JwtHelperService();
    // @ts-ignore
    const tokenDecoded = jwtHelper.decodeToken(token)
    return tokenDecoded['role'] === 'Admin2';
  }
}
