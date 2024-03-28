import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TransactionsPaginatedList} from "../_models/paginatedList/transactionsPaginatedList";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  cache = {};

  constructor(private http: HttpClient) {
  }

  getTransactions(pageNumber: string) {
    if (pageNumber === null) {
      pageNumber = "1";
    }
    // @ts-ignore
    if (this.cache[pageNumber]) {
      // @ts-ignore
      return of(this.cache[pageNumber]);
    }

    return this.http.get<TransactionsPaginatedList>(environment.url + '/transactions?page=' + pageNumber).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.cache[pageNumber] = res;
        return res;
      }
      return null;
    }))
  }

  getTransactionsByUserName(userName: string, pageNumber: string) {
    if (pageNumber === null) {
      pageNumber = "1";
    }
    // @ts-ignore
    if (this.cache[userName + pageNumber]) {
      // @ts-ignore
      return of(this.cache[userName + pageNumber]);
    }

    return this.http.get<TransactionsPaginatedList>(environment.url + '/transactions/history?username=' + userName + '&page=' + pageNumber).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.cache[userName + pageNumber] = res;
        return res;
      }
      return null;
    }))
  }
}
