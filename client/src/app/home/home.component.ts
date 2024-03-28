import {Component, OnInit} from '@angular/core';
import {AuthService} from "../_services/auth.service";
import {InvestmentList} from "../_services/_static/investmentList";
import {PlansCardModel} from "../_models/plansCardModel";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  screenWidth = window.innerWidth;
  plansCard: PlansCardModel[] = [];

  constructor(public authService: AuthService) {
  }

  ngOnInit(): void {
    this.plansCard = InvestmentList.getInvestmentList()
  }

}
