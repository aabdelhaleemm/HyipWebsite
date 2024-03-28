import {Component, OnInit} from '@angular/core';
import {InvestmentService} from "../../_services/investment.service";
import {ActivatedRoute, Router} from "@angular/router";
import {InvestmentPaginatedList} from "../../_models/paginatedList/InvestmentPaginatedList";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {InvestmentDetailsModel} from "../../_models/InvestmentDetailsModel";
import {TransactionStatusParser} from "../../_services/_static/TransactionStatusParser";
import {ToastrService} from "ngx-toastr";
import {AuthService} from "../../_services/auth.service";

@Component({
  selector: 'app-admin-investment',
  templateUrl: './admin-investment.component.html',
  styleUrls: ['./admin-investment.component.css']
})
export class AdminInvestmentComponent implements OnInit {
  investment: InvestmentPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, pageIndex: 1, totalCount: 0, totalPages: 0

  }
  status = 'All';
  page: string | null = '1';
  investmentDetails?: InvestmentDetailsModel;
  cancelSelect = 'select';
  confirmCancel = '';
  investmentCancelId = 0;
  isCanceled = false;

  constructor(private investmentService: InvestmentService, private route: ActivatedRoute, private router: Router,
              private toast: ToastrService, public authService: AuthService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.page = res.get("page")
      this.getInvestment();
    })
  }

  timeDiffCalc(dateFuture: Date) {
    let tmLoc = new Date();
    let x = tmLoc.getTime() + tmLoc.getTimezoneOffset() * 60000;
    let diffInMilliSeconds = Math.abs(Date.parse(dateFuture.toString()) - x) / 1000;
    // calculate days
    const days = Math.floor(diffInMilliSeconds / 86400);
    diffInMilliSeconds -= days * 86400;
    // calculate hours
    const hours = Math.floor(diffInMilliSeconds / 3600) % 24;
    diffInMilliSeconds -= hours * 3600;
    let difference = '';
    if (days > 0) {
      difference += (days === 1) ? `${days} day, ` : `${days} days, `;
    }
    difference += (hours === 0 || hours === 1) ? `${hours} hour, ` : `${hours} hours `;

    return difference;
  }

  getInvestment() {
    if (parseInt(this.page!) < 1) {
      this.page = '1';
    }
    this.investmentService.getInvestmentsByStatus(this.status, this.page!).subscribe(res => {
      if (res) {
        this.investment = res;
      }
    })
  }

  getStatusBg(status: string): string {
    return StatusBgService.getInvestmentBgStatus(status);
  }

  getNewStatusName(status: string): string {
    return TransactionStatusParser.getBetterName(status);
  }

  getStatusBgClass(status: string): string {
    return StatusBgService.getStatusBgClass(status);
  }

  changeStatus() {
    this.getInvestment();
  }

  getDetails(id: number) {
    this.investmentService.getDetailsAdmin(id).subscribe(res => {
      this.investmentDetails = res;
    })
  }

  cancelInvestmentIcon(id: number) {
    this.investmentCancelId = id;
    this.confirmCancel = '';
    this.cancelSelect = 'select'
    this.isCanceled = false;
  }

  cancelInvestment() {
    // @ts-ignore
    this.investmentService.cancelInvestment(this.investmentCancelId).subscribe(res => {
      if (res.status === 200) {
        this.toast.success("Investment Canceled Successfully")
        this.isCanceled = true;
        this.getInvestment();
      }
    }, error => console.log())
  }
}
