import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {DepositService} from "../../_services/deposit.service";
import {DepositsPaginatedList} from "../../_models/paginatedList/DepositsPaginatedList";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-admin-deposit',
  templateUrl: './admin-deposit.component.html',
  styleUrls: ['./admin-deposit.component.css']
})
export class AdminDepositComponent implements OnInit {
  depositsPaginatedList: DepositsPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, pageIndex: 1, totalCount: 0, totalPages: 0

  }
  status = 'All';
  page: string | null = '1';
  currentStatus = '';
  newStatus = 'Select';
  depositId = 0;
  adminFeedBack = '';

  constructor(private depositService: DepositService, private route: ActivatedRoute, private router: Router, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.page = res.get("page")
      this.getDeposits();
    })
  }

  getDeposits() {
    if (parseInt(this.page!) < 1) {
      this.page = '1';
    }
    this.depositService.getDepositsByStatus(this.status, this.page).subscribe(res => {
      if (res) {
        this.depositsPaginatedList = res;
      }
    })
  }

  handleEditDepositClick(id: number, status: string) {
    this.depositId = id;
    this.newStatus = 'Select';
    this.currentStatus = status;
    this.adminFeedBack = '';
  }

  getStatusBg(status: string) {
    return StatusBgService.getStatusBgClass(status)
  }


  changeStatus() {
    this.getDeposits();
  }

  changeNewStatus() {
    this.depositService.changeDepositStatus(this.depositId, this.newStatus, this.adminFeedBack).subscribe(res => {
      if (res) {
        this.toast.success("Deposit Status Changed Successfully")
        this.currentStatus = this.newStatus;
        this.newStatus = 'Select';
        this.adminFeedBack = '';
        this.getDeposits()
      }
    })
  }
}
