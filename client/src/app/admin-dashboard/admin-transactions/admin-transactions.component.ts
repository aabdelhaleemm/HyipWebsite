import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TransactionsPaginatedList} from "../../_models/paginatedList/transactionsPaginatedList";
import {TransactionService} from "../../_services/transaction.service";
import {TransactionStatusParser} from "../../_services/_static/TransactionStatusParser";
import {StatusBgService} from "../../_services/_static/StatusBgService";

@Component({
  selector: 'app-admin-user-history',
  templateUrl: './admin-transactions.component.html',
  styleUrls: ['./admin-transactions.component.css']
})
export class AdminTransactions implements OnInit {
  userName: string | null = '';
  page: string | null = '1';
  transactions: TransactionsPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, pageIndex: 0, totalCount: 0, totalPages: 0
  }

  constructor(private transactionsService: TransactionService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      this.userName = res.get("username")
      this.getTransactions()
    })
  }

  getTransactions() {
    if (this.userName === '' || this.userName === null) {
      return;
    }
    if (this.page === null || this.page === undefined || parseInt(this.page) < 1) {
      this.page = '1';
    }
    this.transactionsService.getTransactionsByUserName(this.userName, this.page!).subscribe(res => {
      if (res) {
        this.transactions = res;
      }
    })
  }

  search() {
    this.router.navigate([], {queryParams: {username: this.userName, page: 1}})
  }

  getNewStatusName(status: string): string {
    return TransactionStatusParser.getBetterName(status);
  }

  getStatusBgClass(status: string): string {
    return StatusBgService.getStatusBgClass(status);
  }

}
