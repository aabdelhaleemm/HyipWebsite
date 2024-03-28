import {Component, OnInit} from '@angular/core';
import {InvestmentList} from "../../_services/_static/investmentList";
import {PlansCardModel} from "../../_models/plansCardModel";
import {UserService} from "../../_services/user.service";
import {InvestmentService} from "../../_services/investment.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-user-plans',
  templateUrl: './user-plans.component.html',
  styleUrls: ['./user-plans.component.css']
})
export class UserPlansComponent implements OnInit {
  plansCard: PlansCardModel[] = [];
  userBalance = 0;
  plan?: string;
  max?: number;
  min?: number;
  displayName?: string;
  investmentAmount = 0;


  constructor(private userService: UserService, private investmentService: InvestmentService, private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.plansCard = InvestmentList.getInvestmentList()
    this.getUserBalance();
  }

  addInvestment() {
    this.investmentService.addNew({"Amount": this.investmentAmount, "Plan": this.plan}).subscribe(res => {
      if (res) {
        this.toast.success("Investment Added")
        this.userBalance -= this.investmentAmount;
        this.investmentAmount = 0;
        this.userService.clearUserBalance();

      } else {
        this.toast.error("Please try again")
      }
    })
  }

  handleStartNewInvestment(plan: string, max: number, min: number, displayName: string) {
    this.investmentAmount = 0;
    this.displayName = displayName;
    this.plan = plan;
    this.max = max;
    this.min = min;

  }

  private getUserBalance() {
    this.userService.getUserBalance().subscribe(res => {
      if (res) {
        this.userBalance = res;
      }
    })
  }
}
