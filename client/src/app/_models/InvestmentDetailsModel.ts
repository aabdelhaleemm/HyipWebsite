import {TransactionModel} from "./transactionModel";
import {InvestmentProfitModel} from "./investmentProfitModel";

export class InvestmentDetailsModel {
  investmentsProfits?: InvestmentProfitModel[];
  transactions?: TransactionModel[];
}
