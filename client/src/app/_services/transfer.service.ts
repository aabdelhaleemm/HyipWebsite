import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {of} from "rxjs";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {TransferPaginatedList} from "../_models/paginatedList/transferPaginatedList";
import {UserService} from "./user.service";

@Injectable({
  providedIn: 'root'
})
export class TransferService {
  sentTransferCache = {};
  receivedTransferCache = {};


  constructor(private http: HttpClient, private userService: UserService) {
  }

  getSentTransfers(page: number) {
    // @ts-ignore
    if (this.sentTransferCache[page]) {
      // @ts-ignore
      return of(this.sentTransferCache[page])
    }
    return this.http.get<TransferPaginatedList>(environment.url + '/transfer/sent?page=' + page).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.sentTransferCache[page] = res;
        return res;
      }
      return null;
    }))
  }

  getReceivedTransfers(page: number) {
    // @ts-ignore
    if (this.receivedTransferCache[page]) {
      // @ts-ignore
      return of(this.receivedTransferCache[page])
    }
    return this.http.get<TransferPaginatedList>(environment.url + '/transfer/received?page=' + page).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.receivedTransferCache[page] = res;
        return res;
      }
      return null;
    }))
  }

  requestCode(username:string) {
    return this.http.post(environment.url + '/transfer/request', {'RecipientUserName':username}, {observe: 'response'}).pipe(map(res => {
      return res.status === 200;
    }))
  }

  newTransfer(model: any) {
    return this.http.post(environment.url + '/transfer', model, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.sentTransferCache = {};
        this.userService.clearUserBalance();
      }
      return res.status === 200;
    }))
  }

}
