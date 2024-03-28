export class TransactionStatusParser {
  public static getBetterName(status: string): string {
    switch (status) {
      case 'DepositRequest':
        return "Deposit Request";
      case 'WithdrawRequest':
        return "Withdraw Request"
      case 'InvestmentStart':
        return "Start New Investment";
      case 'InvestmentFinished':
        return "Investment Finished"
      case 'InvestmentCanceled':
        return "Investment Canceled";
      case 'InvestmentProfit':
        return "Investment Profit";
      case 'ReferenceProfit':
        return "Reference Profit";
      case 'MoneyTransfer':
        return "Money Transfer";
    }
    return "";

  }
}
