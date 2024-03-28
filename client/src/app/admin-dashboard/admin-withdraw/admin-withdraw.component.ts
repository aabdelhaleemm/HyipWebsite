import {Component, OnInit} from '@angular/core';
import {WithdrawService} from "../../_services/withdraw.service";
import {ActivatedRoute, Router} from "@angular/router";
import {WithdrawsPaginatedList} from "../../_models/paginatedList/withdrawsPaginatedList";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-admin-withraw',
  templateUrl: './admin-withdraw.component.html',
  styleUrls: ['./admin-withdraw.component.css']
})
export class AdminWithdrawComponent implements OnInit {
  status = 'All';
  page: string | null = '1';
  withdrawList: WithdrawsPaginatedList = {
    hasNextPage: false,
    hasPreviousPage: false,
    items: [],
    pageIndex: 1,
    totalCount: 0,
    totalPages: 0
  };
  currentStatus = '';
  newStatus = 'Select';
  withdrawId = 0;
  adminFeedBack = '';

  constructor(private withdrawService: WithdrawService, private route: ActivatedRoute, private router: Router, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.page = res.get("page")
      this.getWithdraws();
    })
  }

  getWithdraws() {
    if (parseInt(this.page!) < 1) {
      this.page = '1';
    }
    this.withdrawService.getWithdrawsByStatus(this.status, this.page).subscribe(res => {
      if (res) {
        this.withdrawList = res;
      }
    })
  }

  getStatusBg(status: string) {
    return StatusBgService.getStatusBgClass(status)
  }

  changeNewStatus() {
    this.withdrawService.changeWithdrawStatus(this.withdrawId, this.newStatus, this.adminFeedBack).subscribe(res => {
      if (res) {
        this.toast.success("Withdraw Status Changed Successfully")
        this.currentStatus = this.newStatus;
        this.newStatus = 'Select';
        this.adminFeedBack = '';
        this.getWithdraws()
      }
    })
  }

  changeStatus() {
    this.getWithdraws();
  }

  handleEditWithdrawClick(id: number, status: string) {
    this.withdrawId = id;
    this.newStatus = 'Select';
    this.currentStatus = status;
    this.adminFeedBack = '';
  }
}
