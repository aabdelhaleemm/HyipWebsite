import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {ChartModel} from "../_models/chart/chartModel";
import {Observable, of} from "rxjs";
import {AdminModel} from "../_models/adminModel";
import {StateModel} from "../_models/stateModel";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  chart?: ChartModel;
  adminList?: AdminModel[];
  state?: StateModel;

  constructor(private http: HttpClient) {
  }

  getOverview(): Observable<ChartModel | null> {
    if (this.chart !== null && this.chart !== undefined) {
      return of(this.chart);
    }
    return this.http.get<ChartModel>(environment.url + '/admin/chart').pipe(map(res => {
      if (res) {
        this.chart = res;
        return res;
      }
      return null;
    }))
  }

  getState() {
    if (this.state !== undefined) {
      return of(this.state)
    }
    return this.http.get<StateModel>(environment.url + "/admin/state").pipe(map(res => {
      if (res) {
        this.state = res;
        return res;
      }
      return null;
    }))
  }

  changeState(withdraw: boolean | undefined, deposit: boolean | undefined) {
    return this.http.put(environment.url + '/admin/state', {
      IsWithdrawActive: withdraw,
      IsDepositActive: deposit
    }, {observe: "response"}).pipe(map(res => {
      if (res.status === 200) {
        this.state!.isWithdrawActive = withdraw;
        this.state!.isDepositActive = deposit;
        return true;
      }
      return false;
    }))
  }

  getAll() {
    if (this.adminList !== undefined) {
      return of(this.adminList);
    }
    return this.http.get<AdminModel[]>(environment.url + '/admin/all').pipe(map(res => {
      if (res) {
        this.adminList = res;
        return res;
      }
      return null;
    }))
  }

  updateAdmin(model: any) {
    return this.http.put(environment.url + '/admin/update', model, {observe: "response"}).pipe(map(res => {
      if (res.status === 200) {
        this.adminList = undefined;
      }
      return res.status === 200;
    }))
  }

  addAdmin(model: any) {
    return this.http.post(environment.url + '/admin', model, {observe: "response"}).pipe(map(res => {
      if (res.status === 200) {
        this.adminList = undefined;
      }
      return res.status === 200;
    }))
  }

  deleteAdmin(id: number) {
    return this.http.delete<boolean>(environment.url + '/admin/' + id).pipe(map(res => {
      if (res) {
        this.adminList = undefined;
      }
      return res;
    }))
  }

  changePassword(passwordModel: any) {
    return this.http.put<boolean>(environment.url + '/admin/updatePassword', passwordModel).pipe(map(res => {
      return res;
    }))
  }
}
