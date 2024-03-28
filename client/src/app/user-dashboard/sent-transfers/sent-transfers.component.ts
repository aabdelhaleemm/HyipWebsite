import {Component, OnInit} from '@angular/core';
import {DepositsPaginatedList} from "../../_models/paginatedList/DepositsPaginatedList";
import {DepositService} from "../../_services/deposit.service";
import {ActivatedRoute, Router} from "@angular/router";
import {TransferService} from "../../_services/transfer.service";
import {TransferPaginatedList} from "../../_models/paginatedList/transferPaginatedList";

@Component({
  selector: 'app-sent-transfers',
  templateUrl: './sent-transfers.component.html',
  styleUrls: ['./sent-transfers.component.css']
})
export class SentTransfersComponent implements OnInit {
  page: string | null = '1';
  sentTransfers: TransferPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, items: [], pageIndex: 1, totalCount: 0, totalPages: 0
  }

  constructor(private transferService: TransferService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.getSentTransfers();
    })
  }

  getSentTransfers() {
    if (this.page === null) {
      return;
    }
    // @ts-ignore
    this.transferService.getSentTransfers(this.page).subscribe(res => {
      if (res) {
        this.sentTransfers = res;
      }
    })
  }

}
