import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {of} from "rxjs";
import {WithdrawsPaginatedList} from "../_models/paginatedList/withdrawsPaginatedList";
import {UserService} from "./user.service";
import {StateModel} from "../_models/stateModel";

@Injectable({
  providedIn: 'root'
})
export class WithdrawService {
  cache = {};
  userCache = {};
  state?: StateModel;

  constructor(private http: HttpClient, private userService: UserService) {
  }

  isWithdrawActive() {
    if (this.state !== undefined) {
      return of(this.state.isWithdrawActive)
    }
    return this.http.get<StateModel>(environment.url + '/withdraws/state').pipe(map(res => {
      this.state = res;
      return res.isWithdrawActive;
    }))

  }

  getWithdrawHistory(page: number) {
    // @ts-ignore
    if (this.userCache[page]) {
      // @ts-ignore
      return of(this.userCache[page])
    }
    return this.http.get<WithdrawsPaginatedList>(environment.url + '/withdraws/history?page=' + page).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.userCache[page] = res;
        return res;
      }
      return null;
    }))
  }

  getWithdrawsByStatus(status: string, pageNumber: string | null) {
    // @ts-ignore
    if (pageNumber === null) {
      pageNumber = '1';
    }
    // @ts-ignore
    if (this.cache[status + pageNumber]) {
      // @ts-ignore
      return of(this.cache[status + pageNumber])
    }
    return this.http.get(environment.url + '/withdraws/' + status + '?page=' + pageNumber).pipe(map(res => {
      if (res) {

        // @ts-ignore
        this.cache[status + pageNumber] = res;
        return res;
      }
      return null;
    }))
  }

  changeWithdrawStatus(id: number, newStatus: string, adminFeedBack: string) {
    return this.http.put(environment.url + '/withdraws/WithdrawStatus', {
      'WithdrawId': id,
      'Status': newStatus,
      'FeedBack': adminFeedBack
    }, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.cache = {};
        return true
      }
      return false;

    }))
  }

  addWithdrawRequest(model: any) {
    return this.http.post(environment.url + '/withdraws', model, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.userCache = {}
        this.userService.clearUserBalance();
      }
      return res.status === 200;
    }))
  }
}
