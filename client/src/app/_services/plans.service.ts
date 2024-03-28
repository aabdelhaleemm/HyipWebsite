import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {PlansModel} from "../_models/plansModel";
import {of} from "rxjs";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PlansService {
  cache?: PlansModel[];

  constructor(private http: HttpClient) {
  }

  getPlans() {
    if (this.cache !== undefined) {
      return of(this.cache)
    }
    return this.http.get<PlansModel[]>(environment.url + '/plans').pipe(map(res => {
      if (res) {
        this.cache = res;
        return res;
      }
      return null;
    }))
  }

  changeProfitPercent(model: any) {
    return this.http.put(environment.url + '/plans', model, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.cache = undefined;
      }
      return res.status === 200;
    }))

  }
}
