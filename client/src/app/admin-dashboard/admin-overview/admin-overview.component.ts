import {AfterViewInit, ChangeDetectorRef, Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import {AdminService} from "../../_services/admin.service";
import {ChartModel} from "../../_models/chart/chartModel";
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'app-admin-overview',
  templateUrl: './admin-overview.component.html',
  styleUrls: ['./admin-overview.component.css']
})
export class AdminOverviewComponent implements OnInit, AfterViewInit {
  // @ts-ignore
  @ViewChild('chh') chh: ElementRef<HTMLCanvasElement>;
  chart?: ChartModel;
  chart2 = [{}];

  view: [number, number] = [1200, 520]
  view2: [number, number] = [1200, 260]
  view3: [number, number] = [1200, 260]

  constructor(public adminService: AdminService, @Inject(DOCUMENT) private document: HTMLDocument, private cdr: ChangeDetectorRef) {

  }

  ngAfterViewInit(): void {
    this.resizeChart(this.chh.nativeElement.offsetWidth)
    this.cdr.detectChanges();
  }

  ngOnInit(): void {
    this.adminService.getOverview().subscribe(res => {
      if (res) {
        this.chart = res;
        this.chart2 = [
          {name: 'Total Users', value: res.totalUsers},
          {name: 'Total Deposit', value: res.totalDeposits},
          {name: 'Total Investment', value: res.totalInvestment},
          {name: 'Total Profit', value: res.totalProfit},
          {name: 'Total Withdraw', value: res.totalWithdraw},
          {name: 'Money in website ', value: res.totalDeposits + res.totalProfit - res.totalWithdraw},
        ]
      }
    })
  }

  resizeChart(width: any): void {
    this.view = [width, 520]
    let height = width < 495 ? 500 : 260;
    this.view2 = [width, height]
    this.view3 = [width, height + 100]
  }

}
