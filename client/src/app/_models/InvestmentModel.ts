export class InvestmentModel {
  id: number = 0;
  amount?: number;
  percent?: number;
  startDate?: Date;
  endDate?: Date;
  nextProfitEarningDate?: Date;
  totalProfit?: number;
  cancelReason?: string;
  userName?:string;
  status?: string;
  plan?: string;
  userId?: number;
}
