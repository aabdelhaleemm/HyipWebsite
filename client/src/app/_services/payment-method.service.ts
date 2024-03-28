import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {MoneyTransferCardModel} from "../_models/moneyTransferCardModel";
import {map} from "rxjs/operators";
import {of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PaymentMethodService {
  userDepositMethods?: MoneyTransferCardModel[];

  constructor(private http: HttpClient) {
  }

  getDepositMethods() {
    if (this.userDepositMethods !== undefined) {
      return of(this.userDepositMethods);
    }
    return this.http.get<MoneyTransferCardModel[]>(environment.url + '/Deposit').pipe(map(res => {
      if (res) {
        this.userDepositMethods = res;
        return res;
      }
      return null;
    }))
  }
}
