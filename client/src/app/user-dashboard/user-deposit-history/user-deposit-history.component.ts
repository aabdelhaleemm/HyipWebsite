import {Component, OnInit} from '@angular/core';
import {DepositModel} from "../../_models/depositModel";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {DepositService} from "../../_services/deposit.service";
import {InvestmentPaginatedList} from "../../_models/paginatedList/InvestmentPaginatedList";
import {ActivatedRoute, Router} from "@angular/router";
import {DepositsPaginatedList} from "../../_models/paginatedList/DepositsPaginatedList";

@Component({
  selector: 'app-user-deposit-history',
  templateUrl: './user-deposit-history.component.html',
  styleUrls: ['./user-deposit-history.component.css']
})
export class UserDepositHistoryComponent implements OnInit {
  page: string | null = '1';
  deposits: DepositsPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, items: [], pageIndex: 1, totalCount: 0, totalPages: 0
  }

  constructor(private depositService: DepositService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.getDeposits();
    })
  }

  getDeposits() {
    if (this.page === null) {
      return;
    }
    // @ts-ignore
    this.depositService.getAllDepositHistory(this.page).subscribe(res => {
      if (res) {
        this.deposits = res;
      }
    })
  }

  getStatusBg(status: string) {
    return StatusBgService.getStatusBgClass(status)
  }
}
