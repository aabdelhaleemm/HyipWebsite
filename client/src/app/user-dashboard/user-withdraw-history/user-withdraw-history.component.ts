import {Component, OnInit} from '@angular/core';
import {WithdrawModel} from "../../_models/withdrawModel";
import {WithdrawService} from "../../_services/withdraw.service";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {ActivatedRoute, Router} from "@angular/router";
import {DepositsPaginatedList} from "../../_models/paginatedList/DepositsPaginatedList";
import {WithdrawsPaginatedList} from "../../_models/paginatedList/withdrawsPaginatedList";

@Component({
  selector: 'app-user-withdraw-history',
  templateUrl: './user-withdraw-history.component.html',
  styleUrls: ['./user-withdraw-history.component.css']
})
export class UserWithdrawHistoryComponent implements OnInit {
  page: string | null = '1';
  withdrawHistory: WithdrawsPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, items: [], pageIndex: 1, totalCount: 0, totalPages: 0
  }

  constructor(private withdrawService: WithdrawService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.getWithdraws();
    })
  }

  getWithdraws() {
    if (this.page === null) {
      return;
    }
    // @ts-ignore
    this.withdrawService.getWithdrawHistory(this.page).subscribe(res => {
      if (res) {
        this.withdrawHistory = res;
      }
    })
  }

  getStatusBg(status: string): string {
    return StatusBgService.getStatusBgClass(status);
  }

}
