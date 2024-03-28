import {Component, OnInit} from '@angular/core';
import {TransferService} from "../../_services/transfer.service";
import {ActivatedRoute, Router} from "@angular/router";
import {TransferPaginatedList} from "../../_models/paginatedList/transferPaginatedList";

@Component({
  selector: 'app-received-transfers',
  templateUrl: './received-transfers.component.html',
  styleUrls: ['./received-transfers.component.css']
})
export class ReceivedTransfersComponent implements OnInit {
  page: string | null = '1';
  receivedTransfers: TransferPaginatedList = {
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
      this.getReceivedTransfers();
    })
  }

  private getReceivedTransfers() {
    if (this.page === null) {
      return;
    }

    // @ts-ignore
    this.transferService.getReceivedTransfers(this.page).subscribe(res => {
      if (res) {
        this.receivedTransfers = res;
      }
    })
  }
}
