import {InvestmentChartModel} from "./investmentChartModel";

export class ChartModel {
  investmentChart?: InvestmentChartModel[];
  totalInvestmentsAmount?: InvestmentChartModel[];
  totalInvestmentsCount?: InvestmentChartModel[];
  totalDeposits: number=0;
  totalWithdraw: number=0;
  totalInvestment: number=0;
  totalUsers: number=0;
  totalProfit: number=0;

}
