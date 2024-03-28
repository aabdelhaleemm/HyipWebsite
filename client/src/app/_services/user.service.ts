import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserModel} from "../_models/UserModel";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {Observable, of} from "rxjs";
import {UserReferencesModel} from "../_models/UserReferencesModel";
import {OverviewModel} from "../_models/overviewModel";
import {UserPaginatedList} from "../_models/paginatedList/userPaginatedList";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  cache = {};
  userReferences?: UserReferencesModel;
  overView?: OverviewModel;
  userBalance?: number;
  userListCache = {};


  constructor(private http: HttpClient) {
  }

  getAllUsers(page: string) {
    // @ts-ignore
    if (this.userListCache[page]) {
      // @ts-ignore
      return of(this.userListCache[page])
    }
    return this.http.get<UserPaginatedList>(environment.url + '/user/userslist?page=' + page).pipe(map(res => {
      if (res) {

        // @ts-ignore
        this.userListCache[page] = res;
        return res;
      }
      return null
    }))
  }
  getUserByUserName(userName: string) {
    // @ts-ignore
    if (this.cache[userName]) {
      // @ts-ignore
      return of(this.cache[userName])
    }
    return this.http.get<UserModel>(environment.url + '/user/details?username=' + userName).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.cache[userName] = res;
        return res;
      }
      return null;
    }))
  }

  updateUser(userInfo: any) {
    return this.http.put(environment.url + '/user', userInfo, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.cache = {};
        return true;
      }
      return false;
    }))
  }

  requestResetPassword(email: string) {
    return this.http.post(environment.url + '/user/requestReset', {'email': email}, {observe: 'response'}).pipe(map(res => {
      return res.status === 200;
    }))
  }

  validateResetToken(token: string, email: string) {
    return this.http.get<boolean>(environment.url + '/user/validate?token=' + token + '&email=' + email).pipe(map(res => {
      return res;
    }))
  }

  resetPassword(passwordModel: any) {
    return this.http.post<boolean>(environment.url + '/user/reset', passwordModel).pipe(map(res => {
      return res;
    }))
  }

  changePassword(passwordModel: any) {
    return this.http.put<boolean>(environment.url + '/user/updatePassword', passwordModel).pipe(map(res => {
      return res;
    }))
  }

  getReferences() {
    if (this.userReferences !== undefined) {

      return of(this.userReferences);
    }
    // @ts-ignore
    return this.http.get<UserReferencesModel>(environment.url + '/user/references').pipe(map(res => {
      if (res) {
        this.userReferences = res;
        return res;
      }
    }))
  }

  register(registerForm: any) {
    return this.http.post(environment.url + '/user', registerForm, {observe: 'response'}).pipe(map(res => {
      return res.status === 200;
    }))
  }

  getOverview(): Observable<OverviewModel | null> {
    if (this.overView !== null && this.overView !== undefined) {
      return of(this.overView);
    }
    return this.http.get(environment.url + '/user/overview').pipe(map(res => {
      if (res) {
        this.overView = res;
        return res;
      }
      return null;
    }))
  }

  getUserBalance() {
    if (this.overView?.wallet?.balance !== undefined ) {
      return of(this.overView?.wallet?.balance)
    }
    if (this.userBalance !==undefined) {
      return of(this.userBalance)
    }
    return this.http.get<number>(environment.url + '/wallet/balance').pipe(map(res => {
      this.userBalance = res;
      return res;
    }))
  }

  clearUserBalance() {
    this.overView = undefined;
    this.userBalance = undefined;

  }
}
