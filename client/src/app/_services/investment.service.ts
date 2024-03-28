import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {InvestmentPaginatedList} from "../_models/paginatedList/InvestmentPaginatedList";
import {environment} from "../../environments/environment";
import {map} from "rxjs/operators";
import {of} from "rxjs";
import {InvestmentDetailsModel} from "../_models/InvestmentDetailsModel";

@Injectable({
  providedIn: 'root'
})
export class InvestmentService {
  cache = {};
  detailsCache = {};
  adminCache = {};

  constructor(private http: HttpClient) {
  }

  getInvestmentsByStatus(status: string, pageNumber: string) {
    if (pageNumber === null) {
      pageNumber = "1";
    }
    // @ts-ignore
    if (this.adminCache[status + pageNumber]) {
      // @ts-ignore
      return of(this.adminCache[status + pageNumber]);
    }
    return this.http.get<InvestmentPaginatedList>(environment.url + '/investments/' + status + '?page=' + pageNumber).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.adminCache[status + pageNumber] = res;
        return res;
      }
      return null;
    }))
  }

  getDetailsAdmin(id: number) {
    // @ts-ignore
    if (this.detailsCache[id]) {
      // @ts-ignore
      return of(this.detailsCache[id])
    }
    return this.http.get<InvestmentDetailsModel>(environment.url + '/investments/admin/details/' + id).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.detailsCache[id] = res;
        return res;
      }
      return null;
    }))
  }


  getDetails(id: number) {
    // @ts-ignore
    if (this.detailsCache[id]) {
      // @ts-ignore
      return of(this.detailsCache[id])
    }
    return this.http.get<InvestmentDetailsModel>(environment.url + '/investments/details/' + id).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.detailsCache[id] = res;
        return res;
      }
      return null;
    }))
  }

  getInvestments(pageNumber: string) {
    if (pageNumber === null) {
      pageNumber = "1";
    }
    // @ts-ignore
    if (this.cache[pageNumber]) {
      // @ts-ignore
      return of(this.cache[pageNumber]);
    }
    return this.http.get<InvestmentPaginatedList>(environment.url + '/investments?page=' + pageNumber).pipe(map(res => {
      if (res) {
        // @ts-ignore
        this.cache[pageNumber] = res;
        return res;
      }
      return null;
    }))
  }

  cancelInvestment(id: number) {
    if (id < 1) {
      return of(false);
    }
    return this.http.put(environment.url + '/investments/cancel', {'investmentId': id}, {observe: 'response'}).pipe(map(res => {
      this.adminCache = {};
      return res;
    }))
  }

  addNew(model: any) {
    return this.http.post(environment.url + '/investments', model, {observe: 'response'}).pipe(map(res => {
      if (res.status === 200) {
        this.cache = {};
      }
      return res.status === 200;
    }))
  }
}
