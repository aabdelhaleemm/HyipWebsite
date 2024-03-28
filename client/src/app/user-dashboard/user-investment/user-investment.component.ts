import {Component, OnInit} from '@angular/core';
import {InvestmentPaginatedList} from "../../_models/paginatedList/InvestmentPaginatedList";
import {StatusBgService} from "../../_services/_static/StatusBgService";
import {InvestmentService} from "../../_services/investment.service";
import {ActivatedRoute, Router} from "@angular/router";
import {InvestmentDetailsModel} from "../../_models/InvestmentDetailsModel";
import {TransactionStatusParser} from "../../_services/_static/TransactionStatusParser";

@Component({
  selector: 'app-user-investment',
  templateUrl: './user-investment.component.html',
  styleUrls: ['./user-investment.component.css']
})
export class UserInvestmentComponent implements OnInit {
  page: string | null = '1';
  investment: InvestmentPaginatedList = {
    hasNextPage: false, hasPreviousPage: false, items: [], pageIndex: 1, totalCount: 0, totalPages: 0
  }
  investmentDetails?: InvestmentDetailsModel;

  constructor(private investmentService: InvestmentService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(res => {
      this.page = res.get("page")
      if (res.get("page") === null) {
        this.router.navigate([], {queryParams: {page: 1}});
      }
      this.getInvestments();
    })
  }

  getInvestments() {
    this.investmentService.getInvestments(this.page!).subscribe((res: InvestmentPaginatedList) => {
      if (res) {
        this.investment = res;
      }
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

  getStatusBg(status: string): string {
    return StatusBgService.getInvestmentBgStatus(status);
  }

  getNewStatusName(status: string): string {
    return TransactionStatusParser.getBetterName(status);
  }

  getStatusBgClass(status: string): string {
    return StatusBgService.getStatusBgClass(status);
  }

  getDetails(id: number) {

    this.investmentService.getDetails(id).subscribe(res => {
      this.investmentDetails = res;
    })
  }
}
