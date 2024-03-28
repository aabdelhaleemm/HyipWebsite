import {Component, OnInit} from '@angular/core';
import {TransactionService} from "../../_services/transaction.service";
import {ActivatedRoute, Router} from "@angular/router";
import {TransactionsPaginatedList} from "../../_models/paginatedList/transactionsPaginatedList";
import {TransactionStatusParser} from "../../_services/_static/TransactionStatusParser";
import {StatusBgService} from "../../_services/_static/StatusBgService";

@Component({
  selector: 'app-user-transaction',
  templateUrl: './user-transaction.component.html',
  styleUrls: ['./user-transaction.component.css']
})
export class UserTransactionComponent implements OnInit {
  page: string | null = '1';
  transactions: TransactionsPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, pageIndex: 0, totalCount: 0, totalPages: 0

  }

  constructor(private transactionService: TransactionService, private route: ActivatedRoute, private router: Router
  ) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      if (res.get("page") === null) {
        this.router.navigate([], {
          queryParams: {page: 1},
        });
      }
      this.getTransactions();
    })
  }

  getTransactions() {
    if (parseInt(this.page!) < 1) {
      this.page = '1';
    }
    this.transactionService.getTransactions(this.page!).subscribe(res => {
      if (res !== undefined) {
        this.transactions = res;
      }
    })
  }

  getNewStatusName(status: string): string {
    return TransactionStatusParser.getBetterName(status);
  }

  getStatusBgClass(status: string): string {
    return StatusBgService.getStatusBgClass(status);
  }
}
