import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {DepositModel} from "../_models/depositModel";
import {Observable, of} from "rxjs";
import {DepositsPaginatedList} from "../_models/paginatedList/DepositsPaginatedList";

@Injectable({
  providedIn: 'root'
})
export class DepositService {
  userCache = {};
  cache = {};

  constructor(private http: HttpClient) {
  }

  getAllDepositHistory(page: number) {
    // @ts-ignore
    if (this.userCache[page]) {
      // @ts-ignore
      return of(this.userCache[page]);
    }
    return this.http.get<DepositsPaginatedList>(environment.url + '/deposits/history?page=' + page).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.userCache[page] = res;
        return res;
      }
      return null;
    }))
  }

  getDepositsByStatus(status: string, pageNumber: string | null) {
    // @ts-ignore
    if (pageNumber === null) {
      pageNumber = '1';
    }
    // @ts-ignore
    if (this.cache[status + pageNumber]) {
      // @ts-ignore
      return of(this.cache[status + pageNumber])
    }
    return this.http.get<DepositsPaginatedList>(environment.url + '/deposits/' + status + '?page=' + pageNumber).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.cache[status + pageNumber] = res
        return res;
      }
      return null;
    }))
  }

  changeDepositStatus(id: number, newStatus: string, adminFeedBack: string) {
    return this.http.put(environment.url + '/Deposits/DepositStatus', {
        'DepositId': id,
        'Status': newStatus,
        'FeedBack': adminFeedBack
      },
      {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.cache = {};
        return true;
      }
      return false;
    }))
  }

  requestDeposit(form: FormData) {
    return this.http.post(environment.url + '/deposits', form, {observe: "response"}).pipe(map(res => {
      if (res.status === 200) {
        this.userCache = {};
      }
      return res.status === 200;
    }))
  }
}
